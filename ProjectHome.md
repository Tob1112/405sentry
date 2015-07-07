This is the code needed to interface between the Maxstream Xbee 2.5 series controllers and a host PC to control a sentry gun created by us for our Spring 2008 ECEN 405 senior design project at Texas A&M University.

We are using Andrew Kirillov's excellent AForge.NET C# framework to handle motion detection in our video stream, part of which was heavily modified by us and integrated with other portions of DShowNET to support devices with multiple video inputs and to handle YUYV color space streams.

The sentry sounds we used are taken directly from Valve Software's Portal.

Frameworks/References Used:
  * AForge.NET - http://code.google.com/p/aforge/
  * DShowNET - http://www.codeproject.com/KB/directx/directshownet.aspx
  * DirectX.Capture - http://www.codeproject.com/KB/directx/directxcapture.aspx

Project Inspiration:
  * Portal - http://orange.half-life2.com/portal.html
  * Team Fortress 2 - http://orange.half-life2.com/tf2.html