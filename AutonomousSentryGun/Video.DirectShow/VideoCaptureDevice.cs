namespace AForge.Video.DirectShow
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Runtime.InteropServices;
    using System.Net;
    using DirectX.Capture;
    using AForge.Video;
    using DShowNET;
    //using AForge.Video.DirectShow.Internals;    
    /// <summary>
    /// Video source for local video capture device (for example USB webcam).
    /// </summary>
    /// 
    /// <remarks><para>The video source captures video data from local video capture device.
    /// DirectShow is used for capturing.</para>
    /// <para>Sample usage:</para>
    /// <code>
    /// // enumerate video devices
    /// videoDevices = new FilterInfoCollection( FilterCategory.VideoInputDevice );
    /// // create video source
    /// VideoCaptureDevice videoSource = new VideoCaptureDevice( videoDevices[0].MonikerString );
    /// // set NewFrame event handler
    /// videoSource.NewFrame += new NewFrameEventHandler( video_NewFrame );
    /// // start the video source
    /// videoSource.Start( );
    /// // ...
    /// // signal to stop
    /// videoSource.SignalToStop( );
    /// // ...
    /// 
    /// private void video_NewFrame( object sender, NewFrameEventArgs eventArgs )
    /// {
    ///     // get new frame
    ///     Bitmap bitmap = eventArgs.Frame;
    ///     // process the frame
    /// }
    /// </code>
    /// </remarks>
    /// 
    public class VideoCaptureDevice : IVideoSource
    {
        // moniker string of video capture device
        private string deviceMoniker;
        // user data associated with the video source
        private object userData = null;
        // received frames count
        private int framesReceived;
        // recieved byte count
        private int bytesReceived;
        // prevent freezing
        private bool preventFreezing = true;
        // activate erosion filter
        private bool erosionOn = false;
        
        //Video Modifiers
        //Boolean to activate Input Stream Modification
        private bool modifyStream = false;
        //Size of the video input stream
        private Size streamSize;
        //sets if input is a YUYV stream or not
        private bool YUYV = false;
        //sets the stream's framerate (neither camera supports different frame rates)
        //double framerate = 5;
        
        // Configuration streams
        private DShowNET.IAMStreamConfig videoStreamConfig = null;

        private Thread thread = null;
        private ManualResetEvent stopEvent = null;

        /// <summary>
        /// New frame event.
        /// </summary>
        /// 
        /// <remarks>Notifies client about new available frame from video source.</remarks>
        /// 
        public event NewFrameEventHandler NewFrame;

        /// <summary>
        /// Video source error event.
        /// </summary>
        /// 
        /// <remarks>The event is used to notify client about any type error occurred in
        /// video source object, for example exceptions.</remarks>
        /// 
        public event VideoSourceErrorEventHandler VideoSourceError;

        /// <summary>
        /// Video source.
        /// </summary>
        /// 
        /// <remarks>Video source is represented by moniker string of video capture device.</remarks>
        /// 
        public virtual string Source
        {
            get { return deviceMoniker; }
            set { deviceMoniker = value; }
        }

        public bool getYUYV()
        {
            return YUYV;
        }

        public bool getErosion()
        {
            return erosionOn;
        }

        /// <summary>
        /// Received frames count.
        /// </summary>
        /// 
        /// <remarks>Number of frames the video source provided from the moment of the last
        /// access to the property.
        /// </remarks>
        /// 
        public int FramesReceived
        {
            get
            {
                int frames = framesReceived;
                framesReceived = 0;
                return frames;
            }
        }

        /// <summary>
        /// Received bytes count.
        /// </summary>
        /// 
        /// <remarks>Number of bytes the video source provided from the moment of the last
        /// access to the property.
        /// </remarks>
        /// 
        public int BytesReceived
        {
            get
            {
                int bytes = bytesReceived;
                bytesReceived = 0;
                return bytes;
            }
        }

        /// <summary>
        /// User data.
        /// </summary>
        /// 
        /// <remarks>The property allows to associate user data with video source object.</remarks>
        /// 
        public object UserData
        {
            get { return userData; }
            set { userData = value; }
        }

        /// <summary>
        /// State of the video source.
        /// </summary>
        /// 
        /// <remarks>Current state of video source object.</remarks>
        /// 
        public bool IsRunning
        {
            get
            {
                if ( thread != null )
                {
                    // check thread status
                    if ( thread.Join( 0 ) == false )
                        return true;

                    // the thread is not running, free resources
                    Free( );
                }
                return false;
            }
        }

        /// <summary>
        /// Prevent video freezing after screen saver and workstation lock or not.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>The value specifies if the class should prevent video freezing during and
        /// after screen saver or workstation lock. To prevent freezing the <i>DirectShow</i> graph
        /// should not contain  <i>Renderer</i> filter, which is added by <i>Render()</i> method
        /// of graph. However, in some cases it may be required to call <i>Render()</i> method of graph, since
        /// it may add some more filters, which may be required for playing video. So, the property is
        /// a trade off - it is possible to prevent video freezing skipping adding renderer filter or
        /// it is possible to keep renderer filter, but video may freeze during screen saver.</para>
        /// <para>Default value of this property is set to <b>true</b> for capture device.</para>
        /// <para><note>The property may become obsolete in the future if approach to disable freezing
        /// and adding all required filters is found.</note></para>
        /// <para><note>The property should be set before calling <see cref="Start"/> method
        /// of the class.</note></para>
        /// </remarks>
        /// 
        public bool PreventFreezing
        {
            get { return preventFreezing; }
            set { preventFreezing = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoCaptureDevice"/> class.
        /// </summary>
        /// 
        public VideoCaptureDevice( ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoCaptureDevice"/> class.
        /// </summary>
        /// 
        /// <param name="deviceMoniker">Moniker string of video capture device.</param>
        /// 
        public VideoCaptureDevice( string deviceMoniker )
        {
            this.deviceMoniker = deviceMoniker;
            modifyStream = false;
            YUYV = false;
            erosionOn = false;
        }

        public VideoCaptureDevice(string deviceMoniker, Size streamRes)
        {
            this.deviceMoniker = deviceMoniker;
            modifyStream = true;
            streamSize = streamRes;
            YUYV = false;
            erosionOn = false;
        }

        public VideoCaptureDevice(string deviceMoniker, bool erosion)
        {
            this.deviceMoniker = deviceMoniker;
            modifyStream = false;
            YUYV = false;
            erosionOn = erosion;
        }

        public VideoCaptureDevice(string deviceMoniker, Size streamRes, bool erosion)
        {
            this.deviceMoniker = deviceMoniker;
            modifyStream = true;
            streamSize = streamRes;
            YUYV = false;
            erosionOn = erosion;
        }      

        /// <summary>
        /// Start video source.
        /// </summary>
        /// 
        /// <remarks>Start video source and return execution to caller. Video source
        /// object creates background thread and notifies about new frames with the
        /// help of <see cref="NewFrame"/> event.</remarks>
        /// 
        public void Start( )
        {
            if ( thread == null )
            {
                // check source
                if ( ( deviceMoniker == null ) || ( deviceMoniker == string.Empty ) )
                    throw new ArgumentException( "Video source is not specified" );

                framesReceived = 0;
                bytesReceived = 0;

                // create events
                stopEvent = new ManualResetEvent( false );

                // create and start new thread
                thread = new Thread( new ThreadStart( WorkerThread ) );
                thread.Name = deviceMoniker; // mainly for debugging
                thread.Start( );
            }
        }

        /// <summary>
        /// Signal video source to stop its work.
        /// </summary>
        /// 
        /// <remarks>Signals video source to stop its background thread, stop to
        /// provide new frames and free resources.</remarks>
        /// 
        public void SignalToStop( )
        {
            // stop thread
            if ( thread != null )
            {
                // signal to stop
                stopEvent.Set( );
            }
        }

        /// <summary>
        /// Wait for video source has stopped.
        /// </summary>
        /// 
        /// <remarks>Waits for source stopping after it was signalled to stop using
        /// <see cref="SignalToStop"/> method.</remarks>
        /// 
        public void WaitForStop( )
        {
            if ( thread != null )
            {
                // wait for thread stop
                thread.Join( );

                Free( );
            }
        }

        /// <summary>
        /// Stop video source.
        /// </summary>
        /// 
        /// <remarks>Stops video source aborting its thread.</remarks>
        /// 
        public void Stop( )
        {
            if ( this.IsRunning )
            {
                thread.Abort( );
                WaitForStop( );
            }
        }

        /// <summary>
        /// Free resource.
        /// </summary>
        /// 
        private void Free( )
        {
            thread = null;

            // release events
            stopEvent.Close( );
            stopEvent = null;
        }        
      
        // Thread entry point
        public void WorkerThread()
        {
            int hr;
            Guid cat;
            Guid med;

            // grabber
            Grabber grabber = new Grabber(this);

            // objects
            object graphObj = null;
            object grabberObj = null;

            // interfaces
            IGraphBuilder graphBuilder = null;
            DShowNET.ICaptureGraphBuilder2 captureGraphBuilder = null;
            IBaseFilter videoDeviceFilter = null;
            IBaseFilter grabberFilter = null;            
            ISampleGrabber sg = null;
            IMediaControl mc = null;

            try
            {
                // Make a new filter graph
                graphObj = Activator.CreateInstance(Type.GetTypeFromCLSID(DShowNET.Clsid.FilterGraph, true));
                graphBuilder = (IGraphBuilder)graphObj;

                // Get the Capture Graph Builder
                Guid clsid = DShowNET.Clsid.CaptureGraphBuilder2;
                Guid riid = typeof(DShowNET.ICaptureGraphBuilder2).GUID;
                captureGraphBuilder = (DShowNET.ICaptureGraphBuilder2) DShowNET.DsBugWO.CreateDsInstance(ref clsid, ref riid);

                // Link the CaptureGraphBuilder to the filter graph
                hr = captureGraphBuilder.SetFiltergraph((DShowNET.IGraphBuilder)graphBuilder);
                if (hr < 0) Marshal.ThrowExceptionForHR(hr);

                int rotCookie = 0;
                DShowNET.DsROT.AddGraphToRot(graphBuilder, out rotCookie);

                // Get the video device and add it to the filter graph
                if (deviceMoniker != null)
                {
                    videoDeviceFilter = (IBaseFilter)Marshal.BindToMoniker(deviceMoniker);
                    hr = graphBuilder.AddFilter(videoDeviceFilter,
                    "Video Capture Device");
                    if (hr < 0) Marshal.ThrowExceptionForHR(hr);
                }                                

                // create sample grabber, object and filter
                grabberObj = Activator.CreateInstance(Type.GetTypeFromCLSID(Clsid.SampleGrabber, true));
                grabberFilter = (IBaseFilter)grabberObj;
                sg = (ISampleGrabber)grabberObj;

                // add sample grabber filter to filter graph
                hr = graphBuilder.AddFilter(grabberFilter, "grabber");
                if (hr < 0) Marshal.ThrowExceptionForHR(hr);

                // Try looking for an video device interleaved media type
                IBaseFilter testFilter = videoDeviceFilter;
                // grabberFilter (not supported)
                object o;
                cat = DShowNET.PinCategory.Capture;
                med = DShowNET.MediaType.Interleaved;
                Guid iid = typeof(DShowNET.IAMStreamConfig).GUID;
                hr = captureGraphBuilder.FindInterface(ref cat, ref med, (DShowNET.IBaseFilter)testFilter, ref iid, out o);

                if (hr != 0)
                {
                    // If not found, try looking for a video media type
                    med = MediaType.Video;
                    hr = captureGraphBuilder.FindInterface(
                    ref cat, ref med, (DShowNET.IBaseFilter)testFilter, ref iid, out o);

                    if (hr != 0)
                        o = null;
                }

                // Set the video stream configuration to data member
                videoStreamConfig = o as DShowNET.IAMStreamConfig;
                o = null;
                
                //modifies the stream size and frame rate                
                if (modifyStream)
                {
                    //set size of frame
                    BitmapInfoHeader bmiHeader;
                    bmiHeader = (BitmapInfoHeader)getStreamConfigSetting(videoStreamConfig, "BmiHeader");                    
                    bmiHeader.Width = streamSize.Width;
                    bmiHeader.Height = streamSize.Height;
                    setStreamConfigSetting(videoStreamConfig,"BmiHeader", bmiHeader);

                    //set frame rate (not supported on the cameras we have)
                    /*
                    long avgTimePerFrame = (long)(10000000 / framerate);
                    setStreamConfigSetting(videoStreamConfig, "AvgTimePerFrame", avgTimePerFrame);
                    */
                }

                // connect pins (Turns on the video device)
                if (graphBuilder.Connect((IPin)AForge.Video.DirectShow.Internals.Tools.GetOutPin((AForge.Video.DirectShow.Internals.IBaseFilter)videoDeviceFilter, 0), (IPin) AForge.Video.DirectShow.Internals.Tools.GetInPin((AForge.Video.DirectShow.Internals.IBaseFilter)grabberFilter, 0)) < 0)
                    throw new ApplicationException("Failed connecting filters");
                
                // Set the sample grabber media type settings
                AMMediaType mt = new AMMediaType();                                                  
                mt.majorType = MediaType.Video;
                mt.subType = MediaSubType.RGB24;
                sg.SetMediaType(mt);

                // get media type and set sample grabber parameters
                if (sg.GetConnectedMediaType(mt) == 0)
                {
                    VideoInfoHeader vih = (VideoInfoHeader)Marshal.PtrToStructure(mt.formatPtr, typeof(VideoInfoHeader));
                    if (vih.BmiHeader.Compression != 0)
                    {
                        YUYV = true;
                        grabber.setYUYV(YUYV);
                    }                    
                    grabber.Width = vih.BmiHeader.Width;
                    grabber.Height = vih.BmiHeader.Height;                    
                    //mt.Dispose();
                }
                
                // Set various sample grabber properties
                sg.SetBufferSamples(false);
                sg.SetOneShot(false);
                sg.SetCallback(grabber, 1);                

                if(!preventFreezing)
                {
                    // render
                    graphBuilder.Render((IPin)AForge.Video.DirectShow.Internals.Tools.GetOutPin((AForge.Video.DirectShow.Internals.IBaseFilter) grabberFilter, 0));

                    // Do not show active (source) window
                    IVideoWindow win = (IVideoWindow)graphObj;
                    win.put_AutoShow(0);
                    win = null;
                }
                
                // get media control
                mc = (IMediaControl)graphBuilder;

                // run
                mc.Run();

                while (!stopEvent.WaitOne(0, true))
                {
                    Thread.Sleep(100);
                }
                mc.StopWhenReady();
            }
            // catch any exceptions
            catch (Exception e)
            {
                // provide information to clients
                if (VideoSourceError != null)
                {
                    VideoSourceError(this, new VideoSourceErrorEventArgs(e.Message));
                }
            }
            // finalization block
            finally
            {
                // release all objects
                mc = null;
                graphBuilder = null;
                captureGraphBuilder = null;
                videoDeviceFilter = null;
                grabberFilter = null;
                sg = null;

                if (graphObj != null)
                {
                    Marshal.ReleaseComObject(graphObj);
                    graphObj = null;
                }
                if (grabberObj != null)
                {
                    Marshal.ReleaseComObject(grabberObj);
                    grabberObj = null;
                }
            }
        }

        /*

        /// <summary>
        /// Worker thread.
        /// </summary>
        /// 
        private void WorkerThread( )
        {
            // grabber
            Grabber grabber = new Grabber( this );

            // objects
            object graphObject = null;
            object sourceObject = null;
            object grabberObject = null;

            // interfaces
            IGraphBuilder   graph2 = null;
            IGraphBuilder   graph = null;
            IBaseFilter     sourceBase = null;            
            IBaseFilter     grabberBase = null;
            ISampleGrabber  sampleGrabber = null;
            IMediaControl   mediaControl = null;
            DShowNET.IGraphBuilder graphObject2 = null;
            DShowNET.IBaseFilter sourceBase2 = null;

            try
            {
                // get type for filter graph
                Type type = Type.GetTypeFromCLSID( Clsid.FilterGraph );
                if ( type == null )
                    throw new ApplicationException( "Failed creating filter graph" );

                //get capture graph setup
                Filters filters = new Filters();
                Capture c = new Capture(filters.VideoInputDevices[0], null);
                c.setupGraph();
                graphObject2 = c.gs.getgb();
                graph2 = (IGraphBuilder)graphObject2;
                sourceBase2 = c.gs.getvdf();



                // create filter graph
                graphObject = Activator.CreateInstance( type );
                graph = (IGraphBuilder) graphObject;

                // create source device's object
                sourceObject = FilterInfo.CreateFilter( deviceMoniker );
                if ( sourceObject == null )
                    throw new ApplicationException( "Failed creating device object for moniker" );

                // get base filter interface of source device
                sourceBase = (IBaseFilter) sourceObject;
                //sourceBase = (IBaseFilter) sourceBase2;

                // get type for sample grabber
                type = Type.GetTypeFromCLSID( Clsid.SampleGrabber );
                if ( type == null )
                    throw new ApplicationException( "Failed creating sample grabber" );
                
                // create sample grabber
                grabberObject = Activator.CreateInstance( type );
                sampleGrabber = (ISampleGrabber) grabberObject;
                grabberBase = (IBaseFilter) grabberObject;

                //sampleGrabber = (ISampleGrabber)c.sampleGrabber;
                //grabberBase = (IBaseFilter)c.grabberBase; 

                // add source and grabber filters to graph
                graph.AddFilter( sourceBase, "source" );
                graph.AddFilter( grabberBase, "grabber" );

                // set media type
                AMMediaType mediaType = new AMMediaType( );
                mediaType.MajorType = MediaType.Video;
                mediaType.SubType = MediaSubType.RGB24;
                sampleGrabber.SetMediaType( mediaType );                

                // connect pins
                if ( graph.Connect( Tools.GetOutPin( sourceBase, 0 ), Tools.GetInPin( grabberBase, 0 ) ) < 0 )
                    throw new ApplicationException( "Failed connecting filters" );

				// get media type
                if ( sampleGrabber.GetConnectedMediaType( mediaType ) == 0 )
                {
                    VideoInfoHeader vih = (VideoInfoHeader) Marshal.PtrToStructure( mediaType.FormatPtr, typeof( VideoInfoHeader ) );

                    grabber.Width = vih.BmiHeader.Width;
                    grabber.Height = vih.BmiHeader.Height;
                    mediaType.Dispose( );
                }

                // let's do rendering, if we don't need to prevent freezing
                if ( !preventFreezing )
                {
                    // render pin
                    graph.Render( Tools.GetOutPin( grabberBase, 0 ) );

                    // configure video window
                    IVideoWindow window = (IVideoWindow) graphObject;
                    window.put_AutoShow( false );
                    window = null;
                }

                // configure sample grabber
                sampleGrabber.SetBufferSamples( false );
                sampleGrabber.SetOneShot( false );
                sampleGrabber.SetCallback( grabber, 1 );                

                // get media control
                mediaControl = (IMediaControl) graphObject;
                //mediaControl = (IMediaControl) graph2;
                graphObject.ToString();

                // run
                mediaControl.Run( );

                
                
                while ( !stopEvent.WaitOne( 0, true ) )
                {
                    Thread.Sleep( 100 );
                }
                mediaControl.StopWhenReady( );
            }
            catch ( Exception exception )
            {
                // provide information to clients
                if ( VideoSourceError != null )
                {
                    VideoSourceError( this, new VideoSourceErrorEventArgs( exception.Message ) );
                }
            }
            finally
            {
                // release all objects
                graph           = null;
                sourceBase      = null;
                grabberBase     = null;
                sampleGrabber   = null;
                mediaControl    = null;
                graph2          = null;

                if ( graphObject != null )
                {
                    Marshal.ReleaseComObject( graphObject );
                    graphObject = null;
                }
                if (graphObject2 != null)
                {
                    Marshal.ReleaseComObject(graphObject2);
                    graphObject2 = null;
                }
                if ( sourceObject != null )
                {
                    Marshal.ReleaseComObject( sourceObject );
                    sourceObject = null;
                }
                if ( grabberObject != null )
                {
                    Marshal.ReleaseComObject( grabberObject );
                    grabberObject = null;
                }
            }
        }


        */

        /// <summary>
        /// Notifies client about new frame.
        /// </summary>
        /// 
        /// <param name="image">New frame's image.</param>
        /// 
        protected void OnNewFrame( Bitmap image )
        {            
            framesReceived++;
            if ( ( !stopEvent.WaitOne( 0, true ) ) && ( NewFrame != null ) )
                NewFrame( this, new NewFrameEventArgs( image ) );
        }

        //
        // Vodeo grabber
        //
        private class Grabber : ISampleGrabberCB
        {
            private VideoCaptureDevice parent;
            private int width, height;            
            private AForge.Imaging.Filters.Erosion erosionFilter = new AForge.Imaging.Filters.Erosion();
            private bool YUYV;
            private bool erode;
            private bool grayscale = true;
            private int FrameCount = 0;

            public void setYUYV(bool Y)
            {
                YUYV = Y;
            }

            // Width property
            public int Width
            {
                get { return width; }
                set { width = value; }
            }
            // Height property
            public int Height
            {
                get { return height; }
                set { height = value; }
            }

            // Constructor
            public Grabber( VideoCaptureDevice parent )
            {
                this.parent = parent;
                YUYV = parent.getYUYV();
                erode = parent.getErosion();
            }

            // Callback to receive samples
            public int SampleCB( double sampleTime, IMediaSample pSample )
            {
                return 0;
            }
            
			// Callback method that receives a pointer to the sample buffer
            public int BufferCB( double sampleTime, IntPtr pBuffer, int bufferLen )
            {
                FrameCount++;

                if (FrameCount % 3 == 0)
                {
                    // create new image
                    System.Drawing.Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);

                    // lock bitmap data
                    BitmapData imageData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                    // copy image data / convert from YUY2 to RGB24 if needed
                    if (YUYV)
                    {
                        int l, c;
                        int r, g, b, cr, cg, cb, y2;
                        int r0, b0, r1, b1, y0, u, y1, v;
                        double g0, g1;

                        l = height;
                        unsafe
                        {
                            byte* dst = (byte*)imageData.Scan0.ToPointer();
                            byte* src = (byte*)pBuffer.ToPointer();
                            while (l-- > 0)
                            {
                                c = width >> 1;
                                while (c-- > 0)
                                {
                                    if (!grayscale)
                                    {                                        
                                        y1 = *src++;
                                        cb = ((*src - 128) * 454) >> 8;
                                        cg = (*src++ - 128) * 88;
                                        y2 = *src++;
                                        cr = ((*src - 128) * 359) >> 8;
                                        cg = (cg + (*src++ - 128) * 183) >> 8;

                                        r = y1 + cr;
                                        b = y1 + cb;
                                        g = y1 - cg;

                                        *dst++ = (byte)b;
                                        *dst++ = (byte)g;
                                        *dst++ = (byte)r;

                                        r = y2 + cr;
                                        b = y2 + cb;
                                        g = y2 - cg;

                                        *dst++ = (byte)b;
                                        *dst++ = (byte)g;
                                        *dst++ = (byte)r;
                                        
                                    }

                                    else if (grayscale)
                                    {
                                        y0 = *src++;
                                        u = *src++;
                                        y1 = *src++;
                                        v = *src++;

                                        //r0 = y0 + v - 128;
                                        //b0 = y0 + u - 128;
                                        //g0 = (y0 - .2125 * r0 - .0721 * b0) / .7514;
                                        //r1 = y1 + v - 128;
                                        //b1 = y1 + u - 128;
                                        //g1 = (y1 - .2125 * r1 - .0721 * b1) / .7514;
                                        r0 = y0;
                                        b0 = y0;
                                        g0 = y0;

                                        r1 = y1;
                                        b1 = y1;
                                        g1 = y1;

                                        *dst++ = (byte)b0;
                                        *dst++ = (byte)g0;
                                        *dst++ = (byte)r0;

                                        *dst++ = (byte)b1;
                                        *dst++ = (byte)g1;
                                        *dst++ = (byte)r1;
                                    }
                                }
                            }
                        }

                    }

                    else if (!YUYV)
                    {
                        // copy image data
                        int srcStride = imageData.Stride;
                        int dstStride = imageData.Stride;

                        unsafe
                        {
                            byte* dst = (byte*)imageData.Scan0.ToPointer() + dstStride * (height - 1);
                            byte* src = (byte*)pBuffer.ToPointer();

                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < srcStride; x++)
                                {
                                    *dst++ = *src++;
                                }
                                dst -= 2 * dstStride;
                            }
                        }
                    }
                    // unlock bitmap data
                    image.UnlockBits(imageData);

                    // notify parent                                
                    if (erode)
                    {
                        Bitmap ErodedImage = erosionFilter.Apply(image);
                        parent.OnNewFrame(ErodedImage);
                        ErodedImage.Dispose();
                        image.Dispose();
                    }
                    else if (!erode)
                    {
                        parent.OnNewFrame(image);
                        // release the image
                        image.Dispose();
                    }
                }
                if (FrameCount == 30)
                    FrameCount = 0;
                return 0;
            }
        }

        protected object getStreamConfigSetting(DShowNET.IAMStreamConfig streamConfig, string fieldName)
        {
            if (streamConfig == null)
                throw new NotSupportedException();

            object returnValue = null;
            IntPtr pmt = IntPtr.Zero;
            DShowNET.AMMediaType mediaType = new DShowNET.AMMediaType();

            try
            {
                // Get the current format info
                int hr = streamConfig.GetFormat(out pmt);
                if (hr != 0)
                    Marshal.ThrowExceptionForHR(hr);
                Marshal.PtrToStructure(pmt, mediaType);

                // The formatPtr member points to different structures
                // dependingon the formatType
                object formatStruct;
                if (mediaType.formatType == DShowNET.FormatType.WaveEx)
                    formatStruct = new DShowNET.WaveFormatEx();
                else if (mediaType.formatType == DShowNET.FormatType.VideoInfo)
                    formatStruct = new VideoInfoHeader();
                else if (mediaType.formatType == DShowNET.FormatType.VideoInfo2)
                    formatStruct = new DShowNET.VideoInfoHeader2();
                else
                    throw new NotSupportedException("This device does not support a recognized format block.");

                // Retrieve the nested structure
                Marshal.PtrToStructure(mediaType.formatPtr, formatStruct);

                // Find the required field
                Type structType = formatStruct.GetType();
                 FieldInfo fieldInfo = structType.GetField(fieldName);
                if (fieldInfo == null)
                    throw new NotSupportedException("Unable to find the member '" + fieldName + "' in the format block.");

                // Extract the field's current value
                returnValue = fieldInfo.GetValue(formatStruct);

            }
            finally
            {
                //DsUtils.FreeAMMediaType(mediaType);
                Marshal.FreeCoTaskMem(pmt);
            }

            return (returnValue);
        }

        protected object setStreamConfigSetting(DShowNET.IAMStreamConfig streamConfig, string fieldName, object newValue)
        {
            if (streamConfig == null)
                throw new NotSupportedException();

            object returnValue = null;
            IntPtr pmt = IntPtr.Zero;
            DShowNET.AMMediaType mediaType = new DShowNET.AMMediaType();

            try
            {
                // Get the current format info
                int hr = streamConfig.GetFormat(out pmt);
                if (hr != 0)
                    Marshal.ThrowExceptionForHR(hr);
                Marshal.PtrToStructure(pmt, mediaType);

                // The formatPtr member points to different structures
                // dependingon the formatType
                object formatStruct;
                if (mediaType.formatType == DShowNET.FormatType.WaveEx)
                    formatStruct = new DShowNET.WaveFormatEx();
                else if (mediaType.formatType == DShowNET.FormatType.VideoInfo)
                    formatStruct = new VideoInfoHeader();
                else if (mediaType.formatType == DShowNET.FormatType.VideoInfo2)
                    formatStruct = new DShowNET.VideoInfoHeader2();
                else
                    throw new NotSupportedException("This device does not support a recognized format block.");

                // Retrieve the nested structure
                Marshal.PtrToStructure(mediaType.formatPtr, formatStruct);

                // Find the required field
                Type structType = formatStruct.GetType();
                FieldInfo fieldInfo = structType.GetField(fieldName);
                if (fieldInfo == null)
                    throw new NotSupportedException("Unable to find the member '" + fieldName + "' in the format block.");

                // Update the value of the field
                fieldInfo.SetValue(formatStruct, newValue);

                // PtrToStructure copies the data so we need to copy it back
                Marshal.StructureToPtr(formatStruct, mediaType.formatPtr, false);

                // Save the changes
                hr = streamConfig.SetFormat(mediaType);
                if (hr != 0)
                    Marshal.ThrowExceptionForHR(hr);
            }
            finally
            {
                //DShowNET.DsUtils.FreeAMMediaType(mediaType);
                Marshal.FreeCoTaskMem(pmt);
            }

            return (returnValue);
        }

    }
}