using System;
using Microsoft.Xna.Framework.Graphics;

// This file is required to get text working in XNAPopupWindow.cs

// super hacked up.  Probably unstable as shit! Be warned!
// to get the spritefont to work I had to:
// download a .spritefont file (basically just XML)
// Use XNAFormatter to convert it into a .xnb file
// add the file to the project and set the CopyToOutputDirectory setting to copyalways
// set up the contentmanager like so:
/*
 * IServiceProvider isp = new ServiceCollection();
            CustomGraphicsDeviceService gds = new CustomGraphicsDeviceService();
            gds.GraphicsDevice = dev;
            ((ServiceCollection)isp).AddService<IGraphicsDeviceService>(gds);
            contentManager = new ContentManager(isp);
            try
            {
                font = contentManager.Load<SpriteFont>("Courier");
            }
            catch
            {
                Console.WriteLine("File not found!");
            }
 */
// and voila!

namespace SharedGraphicsClasses
{
    class CustomGraphicsDeviceService : IGraphicsDeviceService
    {
        public GraphicsDevice GraphicsDevice { get; set; }

        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        public CustomGraphicsDeviceService()
        {
            DeviceCreated = delegate { CustomGraphicsDeviceService_DeviceCreated(); };
            DeviceDisposing = delegate { CustomGraphicsDeviceService_DeviceDisposing(); };
            DeviceReset = delegate { CustomGraphicsDeviceService_DeviceReset(); };
            DeviceResetting = delegate { CustomGraphicsDeviceService_DeviceResetting(); };
        }

        public void CustomGraphicsDeviceService_DeviceCreated()
        {
            //Console.WriteLine("");
        }

        public void CustomGraphicsDeviceService_DeviceDisposing()
        {
            //Console.WriteLine("");
        }

        public void CustomGraphicsDeviceService_DeviceReset()
        {
            //Console.WriteLine("");
        }

        public void CustomGraphicsDeviceService_DeviceResetting()
        {
            //Console.WriteLine("");
        }
    }
}
