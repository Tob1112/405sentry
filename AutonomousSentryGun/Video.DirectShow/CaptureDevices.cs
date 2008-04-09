// Motion Detector
//
// Copyright © Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//
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
    using DirectX.Capture;

    using AForge.Video;
    using AForge.Video.DirectShow.Internals;
    using DShowNET;


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
        //public event CameraEventHandler NewFrame;

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
                if (bdebug)
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
                    VideoInfoHeader vih = (VideoInfoHeader)Marshal.PtrToStructure(mt.formatPtr, typeof(VideoInfoHeader));
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
                IVideoWindow win = (IVideoWindow)graphObj;
                win.put_AutoShow(false);
                win = null;

                // get media control
                mc = (IMediaControl)graphObj;

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