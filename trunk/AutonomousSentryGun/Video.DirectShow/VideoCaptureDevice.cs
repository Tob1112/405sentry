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
    using AForge.Video.DirectShow.Internals;    
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
            IBaseFilter aviFilter = null;
            IBaseFilter YUY2RGBFilter = null;
            IBaseFilter CSCFilter = null;
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

                /*aviFilter = (IBaseFilter)Marshal.BindToMoniker("@device:sw:{083863F1-70DE-11D0-BD40-00A0C911CE86}\\{4F3E50BD-A9D7-4721-B0E1-00CB42A0A747}");
                hr = graphBuilder.AddFilter(aviFilter, "AVI Decompressor");
                if (hr < 0) Marshal.ThrowExceptionForHR(hr);

                YUY2RGBFilter = (IBaseFilter)Marshal.BindToMoniker("@device:sw:{083863F1-70DE-11D0-BD40-00A0C911CE86}\\{CC58E280-8AA1-11D1-B3F1-00AA003761C5}");
                hr = graphBuilder.AddFilter(YUY2RGBFilter, "Smart Tee");
                if (hr < 0) Marshal.ThrowExceptionForHR(hr);*/

                //CSCFilter = (IBaseFilter)Marshal.BindToMoniker("@device:sw:{083863F1-70DE-11D0-BD40-00A0C911CE86}\\{1643E180-90F5-11CE-97D5-00AA0055595A}");
                //hr = graphBuilder.AddFilter(CSCFilter, "Color Space Converter");
                //if (hr < 0) Marshal.ThrowExceptionForHR(hr);

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

                //need to change size of stream to 320x240 to speed up motion detection
                bool bdebug =false;
                if (bdebug)
                {
                    DShowNET.BitmapInfoHeader bmiHeader;
                    bmiHeader = (DShowNET.BitmapInfoHeader)getStreamConfigSetting(videoStreamConfig, "BmiHeader");
                    
                    bmiHeader.Width = 320;
                    bmiHeader.Height = 240;
                    setStreamConfigSetting(videoStreamConfig,"BmiHeader", bmiHeader);
                }

                // connect pins (Turns on the video device)
                //if (graphBuilder.Connect(Tools.GetOutPin(videoDeviceFilter, 0), Tools.GetInPin(aviFilter, 0)) < 0)
                //    throw new ApplicationException("Failed connecting filters");

                // connect pins (Turns on the video device)
                //if (graphBuilder.Connect(Tools.GetOutPin(videoDeviceFilter, 0), Tools.GetInPin(CSCFilter, 0)) < 0)
                //    throw new ApplicationException("Failed connecting filters");

                // connect pins (Turns on the video device)
                if (graphBuilder.Connect(Tools.GetOutPin(videoDeviceFilter, 0), Tools.GetInPin(grabberFilter, 0)) < 0)
                    throw new ApplicationException("Failed connecting filters");

                bool YUYV = false;
                AMMediaType mt = new AMMediaType();
                // Set the sample grabber media type settings
                if (YUYV)
                {                    
                    mt.MajorType = MediaType.Video;
                    mt.SubType = MediaSubType.YUYV;
                    sg.SetMediaType(mt);
                }
                else if (!YUYV)
                {                    
                    mt.MajorType = MediaType.Video;
                    mt.SubType = MediaSubType.RGB24;
                    sg.SetMediaType(mt);
                }
                
                // get media type
                if (sg.GetConnectedMediaType(mt) == 0)
                {
                    VideoInfoHeader vih = (VideoInfoHeader)Marshal.PtrToStructure(mt.FormatPtr, typeof(VideoInfoHeader));
                    //System.Diagnostics.Debug.WriteLine("width = " + vih.BmiHeader.Width + ", height = " + vih.BmiHeader.Height);
                    vih.BmiHeader.Compression.ToString();
                    grabber.Width = vih.BmiHeader.Width;
                    grabber.Height = vih.BmiHeader.Height;                    
                    //setStreamConfigSetting(videoStreamConfig, "BmiHeader", vih.BmiHeader);
                    mt.Dispose();
                }
                
                // Set various sample grabber properties
                sg.SetBufferSamples(false);
                sg.SetOneShot(false);
                sg.SetCallback(grabber, 1);                

                if(!preventFreezing)
                {
                    // render
                    graphBuilder.Render(Tools.GetOutPin(grabberFilter, 0));


                    // Do not show active (source) window
                    IVideoWindow win = (IVideoWindow)graphObj;
                    win.put_AutoShow(false);
                    win = null;
                }
                /*
                cat = DShowNET.PinCategory.Preview;
                med = DShowNET.MediaType.Video;
                hr = captureGraphBuilder.RenderStream(ref cat, ref med, videoDeviceFilter, null, null);
                if (hr < 0) Marshal.ThrowExceptionForHR(hr);*/        

                
                //graphBuilder.Render(Tools.GetOutPin(grabberFilter, 0));
                /*IVideoWindow win = (IVideoWindow)graphObj;
                win.put_AutoShow(true);
                win.put_Visible(true);*/

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
                System.Diagnostics.Debug.WriteLine("----: " + e.Message);
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
            }

            // Callback to receive samples
            public int SampleCB( double sampleTime, IntPtr sample )
            {
                return 0;
            }            

			// Callback method that receives a pointer to the sample buffer
            public int BufferCB( double sampleTime, IntPtr buffer, int bufferLen )
            {
                
                // create new image
                System.Drawing.Bitmap image = new Bitmap( width, height, PixelFormat.Format24bppRgb );
                
                // lock bitmap data
                BitmapData imageData = image.LockBits(
                    new Rectangle( 0, 0, width, height ),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb );

                bool YUYV = true;

                // copy image data / convert from YUY2 to RGB24 if needed
                if (YUYV)
                {
                    int l, c;
                    int r, g, b, cr, cg, cb, y1, y2;

                    l = height;
                    unsafe
                    {
                        byte* dst = (byte*)imageData.Scan0.ToPointer();
                        byte* src = (byte*)buffer.ToPointer();
                        while (l-- > 0)
                        {
                            c = width >> 1;
                            while (c-- > 0)
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
                        }
                        
                        int srcStride = imageData.Stride;
                        int dstStride = imageData.Stride;

                        //int dst2 = imageData.Scan0.ToInt32() + dstStride * (height - 1);
                        //int src = buffer.ToInt32();                    
                        /*
                        for (int y = 0; y < height; y++)
                        {
                            Win32.memcpy(dst2, *src, srcStride);
                            dst -= dstStride;
                            src += srcStride;
                        }*/
                    }
                }

                else if (!YUYV)
                {                    
                    // copy image data
                    int srcStride = imageData.Stride;
                    int dstStride = imageData.Stride;
                
                    int dst = imageData.Scan0.ToInt32( ) + dstStride * ( height - 1 );
                    int src = buffer.ToInt32( );

                    for ( int y = 0; y < height; y++ )
                    {
                        Win32.memcpy( dst, src, srcStride );
                        dst -= dstStride;
                        src += srcStride;
                    }
                }
                // unlock bitmap data
                image.UnlockBits( imageData );

                // notify parent
                bool erode = false;                
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

//older version (use for hopefully making size change work):

/*

At this time, I was able to change the videoDevice's stream
configuration from my frame default format size of 358x288
to 640x480 but no matter what size I choose, the grabber
image always breaks up from one image into 6 images, flipped
about the hortizontal, black and white and it appears in what
it looks like 4 rows; the first row is ripped up (no images seen)
the 2nd row has 3, 160x120 images, the 3rd row same as 1st row,
and the 4th row same as 2nd row.

 namespace VideoSource
{
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;

using dshow;
using dshow.Core;

///
/// CaptureDevice - capture video from local device
///
public class CaptureDevice : IVideoSource
{
private string source;
private double framerate;
private Size framesize;
private object userData = null;
private int framesReceived;

// Configuration streams
private IAMStreamConfig videoStreamConfig = null;

private Thread thread = null;
private ManualResetEvent stopEvent = null;

// new frame event
public event CameraEventHandler NewFrame;

// VideoSource property
public virtual string VideoSource
{
get { return source; }
set { source = value; }
}

// Frame Rate
public double FrameRate
{
get { return framerate; }
set { framerate = value; }
}

// Frame Size
public Size FrameSize
{
get { return framesize; }
set { framesize = value; }
}

// Login property
public string Login
{
get { return null; }
set { }
}
// Password property
public string Password
{
get { return null; }
set { }
}
// FramesReceived property
public int FramesReceived
{
get
{
int frames = framesReceived;
framesReceived = 0;
return frames;
}
}
// BytesReceived property
public int BytesReceived
{
get { return 0; }
}
// UserData property
public object UserData
{
get { return userData; }
set { userData = value; }
}
// Get state of the video source thread
public bool Running
{
get
{
if (thread != null)
{
if (thread.Join(0) == false)
return true;

// the thread is not running, so free resources
Free();
}
return false;
}
}


// Constructor
public CaptureDevice()
{
}

// Start work
public void Start()
{
if (thread == null)
{
framesReceived = 0;

// create events
stopEvent = new ManualResetEvent(false);

// create and start new thread
thread = new Thread(new ThreadStart(WorkerThread));
thread.Name = source;
thread.Start();
}
}

// Signal thread to stop work
public void SignalToStop()
{
// stop thread
if (thread != null)
{
// signal to stop
stopEvent.Set();
}
}

// Wait for thread stop
public void WaitForStop()
{
if (thread != null)
{
// wait for thread stop
thread.Join();

Free();
}
}

// Abort thread
public void Stop()
{
if (this.Running)
{
thread.Abort();
// WaitForStop();
}
}

// Free resources
private void Free()
{
thread = null;

// release events
stopEvent.Close();
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
ICaptureGraphBuilder2 captureGraphBuilder = null;
IBaseFilter videoDeviceFilter = null;
IBaseFilter grabberFilter = null;
ISampleGrabber sg = null;
IMediaControl mc = null;

try
{
// Make a new filter graph
graphObj = Activator.CreateInstance(
Type.GetTypeFromCLSID(Clsid.FilterGraph, true));
graphBuilder = (IGraphBuilder)graphObj;

// Get the Capture Graph Builder
Guid clsid = Clsid.CaptureGraphBuilder2;
Guid riid = typeof(ICaptureGraphBuilder2).GUID;
captureGraphBuilder = (ICaptureGraphBuilder2)
TempFix.CreateDsInstance(ref clsid, ref riid);

// Link the CaptureGraphBuilder to the filter graph
hr = captureGraphBuilder.SetFiltergraph(graphBuilder);
if (hr < 0) Marshal.ThrowExceptionForHR(hr);

// Get the video device and add it to the filter graph
if (source != null)
{
videoDeviceFilter = (IBaseFilter)
Marshal.BindToMoniker(source);
hr = graphBuilder.AddFilter(videoDeviceFilter,
"Video Capture Device");
if (hr < 0) Marshal.ThrowExceptionForHR(hr);
}

// create sample grabber, object and filter
grabberObj = Activator.CreateInstance(
Type.GetTypeFromCLSID(Clsid.SampleGrabber, true));
grabberFilter = (IBaseFilter)grabberObj;
sg = (ISampleGrabber)grabberObj;

// add sample grabber filter to filter graph
hr = graphBuilder.AddFilter(grabberFilter, "grabber");
if (hr < 0) Marshal.ThrowExceptionForHR(hr);

// Try looking for an video device interleaved media type
IBaseFilter testFilter = videoDeviceFilter;
// grabberFilter (not supported)
object o;
cat = PinCategory.Capture;
med = MediaType.Interleaved;
Guid iid = typeof(IAMStreamConfig).GUID;
hr = captureGraphBuilder.FindInterface(
ref cat, ref med, testFilter, ref iid, out o);

if (hr != 0)
{
// If not found, try looking for a video media type
med = MediaType.Video;
hr = captureGraphBuilder.FindInterface(
ref cat, ref med, testFilter, ref iid, out o);

if (hr != 0)
o = null;
}
// Set the video stream configuration to data member
videoStreamConfig = o as IAMStreamConfig;
o = null;

// Experimental testing: Try to set the Frame Size & Rate
// Results: When enabled, the grabber video breaks up into
// several duplicate frames (6 frames)
bool bdebug = true;
if(bdebug)
{
BitmapInfoHeader bmiHeader;
bmiHeader = (BitmapInfoHeader)
getStreamConfigSetting(videoStreamConfig, "BmiHeader");
bmiHeader.Width = framesize.Width;
bmiHeader.Height = framesize.Height;
setStreamConfigSetting(videoStreamConfig,
"BmiHeader", bmiHeader);

long avgTimePerFrame = (long)(10000000 / framerate);
setStreamConfigSetting(videoStreamConfig,
"AvgTimePerFrame", avgTimePerFrame);
}

// connect pins (Turns on the video device)
if (graphBuilder.Connect(DSTools.GetOutPin(
videoDeviceFilter, 0),
DSTools.GetInPin(grabberFilter, 0)) < 0)
throw new ApplicationException(
"Failed connecting filters");

// Set the sample grabber media type settings
AMMediaType mt = new AMMediaType();
mt.majorType = MediaType.Video;
mt.subType = MediaSubType.RGB24;
sg.SetMediaType(mt);

// get media type
if (sg.GetConnectedMediaType(mt) == 0)
{
VideoInfoHeader vih = (VideoInfoHeader) Marshal.PtrToStructure(mt.formatPtr, typeof(VideoInfoHeader));
System.Diagnostics.Debug.WriteLine("width = " + vih.BmiHeader.Width + ", height = " + vih.BmiHeader.Height);
grabber.Width = vih.BmiHeader.Width;
grabber.Height = vih.BmiHeader.Height;
mt.Dispose();
}

// render
graphBuilder.Render(DSTools.GetOutPin(grabberFilter, 0));

// Set various sample grabber properties
sg.SetBufferSamples(false);
sg.SetOneShot(false);
sg.SetCallback(grabber, 1);

// Do not show active (source) window
IVideoWindow win = (IVideoWindow) graphObj;
win.put_AutoShow(false);
win = null;

// get media control
mc = (IMediaControl) graphObj;

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
System.Diagnostics.Debug.WriteLine("----: " + e.Message);
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

// new frame
protected void OnNewFrame(Bitmap image)
{
framesReceived++;
if ((!stopEvent.WaitOne(0, true)) && (NewFrame != null))
NewFrame(this, new CameraEventArgs(image));
}

// Grabber
private class Grabber : ISampleGrabberCB
{
private CaptureDevice parent;
private int width, height;

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
public Grabber(CaptureDevice parent)
{
this.parent = parent;
}

//
public int SampleCB(double SampleTime, IntPtr pSample)
{
return 0;
}

// Callback method that receives a pointer to the sample buffer
public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
{
// create new image
System.Drawing.Bitmap img = new Bitmap(width, height, PixelFormat.Format24bppRgb);

// lock bitmap data
BitmapData bmData = img.LockBits(
new Rectangle(0, 0, width, height),
ImageLockMode.ReadWrite,
PixelFormat.Format24bppRgb);

// copy image data
int srcStride = bmData.Stride;
int dstStride = bmData.Stride;

int dst = bmData.Scan0.ToInt32() + dstStride * (height - 1);
int src = pBuffer.ToInt32();

for (int y = 0; y < height; y++)
{
Win32.memcpy(dst, src, srcStride);
dst -= dstStride;
src += srcStride;
}

// unlock bitmap data
img.UnlockBits(bmData);

// notify parent
parent.OnNewFrame(img);

// release the image
img.Dispose();

return 0;
}
}

protected object getStreamConfigSetting(IAMStreamConfig streamConfig, string fieldName)
{
if (streamConfig == null)
throw new NotSupportedException();

object returnValue = null;
IntPtr pmt = IntPtr.Zero;
AMMediaType mediaType = new AMMediaType();

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
if (mediaType.formatType == FormatType.WaveEx)
formatStruct = new WaveFormatEx();
else if (mediaType.formatType == FormatType.VideoInfo)
formatStruct = new VideoInfoHeader();
else if (mediaType.formatType == FormatType.VideoInfo2)
formatStruct = new VideoInfoHeader2();
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

protected object setStreamConfigSetting(IAMStreamConfig streamConfig, string fieldName, object newValue)
{
if (streamConfig == null)
throw new NotSupportedException();

object returnValue = null;
IntPtr pmt = IntPtr.Zero;
AMMediaType mediaType = new AMMediaType();

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
if (mediaType.formatType == FormatType.WaveEx)
formatStruct = new WaveFormatEx();
else if (mediaType.formatType == FormatType.VideoInfo)
formatStruct = new VideoInfoHeader();
else if (mediaType.formatType == FormatType.VideoInfo2)
formatStruct = new VideoInfoHeader2();
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
//DsUtils.FreeAMMediaType(mediaType);
Marshal.FreeCoTaskMem(pmt);
}

return (returnValue);
}

} // Main Class
} // Namespace

 
 
 */