using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using SharedGraphicsClasses;

// adapted from http://stackoverflow.com/questions/11077236/transparent-window-layer-that-is-click-through-and-always-stays-on-top

namespace DiamondDiggerBot
{
    public class XNAPopupWindow : Form
    {
        // XNA stuff
        GraphicsDevice dev = null;
        BasicEffect effect = null;
        ContentManager contentManager;
        SpriteFont font;

        Rectangle fishingRectangle;

        Texture2D circleTexture = null;
        Texture2D squareTexture = null;
        Texture2D moeTexture = null;

        int colorIndex = 0;

        public const int STATUS_BAR_HEIGHT = 50;
        public const int STATUS_BAR_CHARACTERS_PER_LINE = 65;
        String statusBarText = "";

        public static int IMAGE_SIZE = 40;

        public XNAPopupWindow()
        {
            this.Location = new System.Drawing.Point(0, 0);
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders
            // the formborderstyle MUST be set before setting the size, otherwise this.size will get overwritten
            this.Size = new System.Drawing.Size(500, 500);

            this.TopMost = true;        // make the form always on top                     
            this.Visible = true;        // Important! if this isn't set, then the form is not shown at all

            SetupCameraTexturesAndFonts();

            // Extend aero glass style on form init
            OnResize(null);
        }

        public void SetupCameraTexturesAndFonts()
        {
            // Set the form click-through
            
            //int initialStyle = GetWindowLong(this.Handle, -20);
            //SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            // Create device presentation parameters
            PresentationParameters p = new PresentationParameters();
            p.IsFullScreen = false;
            p.DeviceWindowHandle = this.Handle;
            p.BackBufferFormat = SurfaceFormat.Vector4;
            p.PresentationInterval = PresentInterval.One;

            // Create XNA graphics device
            dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, p);


            //dev.BlendState = BlendState.AlphaBlend;

            // Init basic effect
            effect = new BasicEffect(dev);

            //Image2Texture(global::Skype.Properties.Resources.ILTRight1, dev, ref rightArrow);
            //Image2Texture(global::fishnado.Properties.Resources.Black, dev, ref blackTexture);
            //Image2Texture(global::fishnado.Properties.Resources.White, dev, ref whiteTexture);
            Image2Texture(System.Drawing.Image.FromFile("Graphics/Images/WhiteCircle.png"), dev, ref circleTexture);
            Image2Texture(System.Drawing.Image.FromFile("Graphics/Images/WhiteSquare.png"), dev, ref squareTexture);
            Image2Texture(System.Drawing.Image.FromFile("Graphics/Images/moe.png"), dev, ref moeTexture);

            IServiceProvider isp = new ServiceCollection();
            CustomGraphicsDeviceService gds = new CustomGraphicsDeviceService();
            gds.GraphicsDevice = dev;
            ((ServiceCollection)isp).AddService<IGraphicsDeviceService>(gds);
            contentManager = new ContentManager(isp);
            font = contentManager.Load<SpriteFont>("Graphics/SpriteFonts/Courier");
        }

        protected override void OnResize(EventArgs e)
        {
            int[] margins = new int[] { 0, 0, Width, Height };

            // Extend aero glass style to whole form
            DwmExtendFrameIntoClientArea(this.Handle, ref margins);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing here to stop window normal background painting
        }

        public void UpdateWindowRect(System.Drawing.Rectangle rect)
        {
            this.Location = rect.Location;
            this.Size = new System.Drawing.Size(rect.Width, rect.Height + STATUS_BAR_HEIGHT);

            SetupCameraTexturesAndFonts(); // pretty sure this operation is expensive, so don't call often

            OnResize(null);
            Invalidate();
        }

        public void UpdatePopupWindow(Microsoft.Xna.Framework.Rectangle fishingRect, String text)
        {
            fishingRectangle = fishingRect;
            Console.WriteLine("New Rectangle: " + fishingRectangle);
            naivePackStringIntoStatusBar(text);

            OnResize(null);
            Invalidate();
        }

        // smart packing would look for whitespace
        public void naivePackStringIntoStatusBar(String text)
        {
            if (text != null)
            {
                if (text.Length > STATUS_BAR_CHARACTERS_PER_LINE * 3)
                {
                    text = text.Insert(STATUS_BAR_CHARACTERS_PER_LINE * 2, "\n");
                    text = text.Insert(STATUS_BAR_CHARACTERS_PER_LINE * 1, "\n");
                    statusBarText = text.Substring(0, STATUS_BAR_CHARACTERS_PER_LINE * 3) + "...";
                }
                else if (text.Length > STATUS_BAR_CHARACTERS_PER_LINE * 2)
                {
                    text = text.Insert(STATUS_BAR_CHARACTERS_PER_LINE * 2, "\n");
                    text = text.Insert(STATUS_BAR_CHARACTERS_PER_LINE * 1, "\n");
                    statusBarText = text;
                }
                else if (text.Length > STATUS_BAR_CHARACTERS_PER_LINE)
                {
                    text = text.Insert(STATUS_BAR_CHARACTERS_PER_LINE * 1, "\n");
                    statusBarText = text;
                }
                else
                {
                    statusBarText = text;
                }
            }
        }

        public Color getArrowBrushColor()
        {
            colorIndex++;

            if (colorIndex == 0)
            {
                return Color.Red;
            }else if (colorIndex == 1)
            {
                return Color.Yellow;
            }else if (colorIndex == 2)
            {
                return Color.Blue;
            }else //if (colorIndex == 3)
            {
                colorIndex = -1;
                return Color.Green;
            }
        }

        private void DrawBackground(SpriteBatch sb)
        {
            sb.Draw(squareTexture, new Rectangle(0, 0, this.Width, this.Height), Color.Black);
        }

        private void DrawAlek(SpriteBatch sb)
        {
            sb.Draw(moeTexture, new Rectangle(25, 100, 450, 278), Color.White);
        }

        private void DrawStatusBar(SpriteBatch sb)
        {
            sb.Draw(squareTexture, new Rectangle(0, this.Height, this.Width, STATUS_BAR_HEIGHT), Color.Black);
            sb.DrawString(font, statusBarText, new Vector2(0, this.Height - STATUS_BAR_HEIGHT), Color.Green);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //dev.Clear(new Color(0, 0, 0, 0.0f));

            // need this anymore??
            // Enable position colored vertex rendering
            effect.VertexColorEnabled = true;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) pass.Apply();


            SpriteBatch sb = new SpriteBatch(dev);

            sb.Begin();

            DrawBackground(sb);
            DrawAlek(sb);
            DrawStatusBar(sb);

            sb.End();

            // Present the device contents into form
            dev.Present();
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        public static void Image2Texture(System.Drawing.Image image,
                                 GraphicsDevice graphics,
                                 ref Texture2D texture)
        {
            if (image == null)
            {
                return;
            }

            if (texture == null || texture.IsDisposed ||
                texture.Width != image.Width ||
                texture.Height != image.Height ||
                texture.Format != SurfaceFormat.Color)
            {
                if (texture != null && !texture.IsDisposed)
                {
                    texture.Dispose();
                }

                texture = new Texture2D(graphics, image.Width, image.Height, false, SurfaceFormat.Color);
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    if (graphics.Textures[i] == texture)
                    {
                        graphics.Textures[i] = null;
                        break;
                    }
                }
            }

            //Memory stream to store the bitmap data.
            MemoryStream ms = new MemoryStream();

            //Save to that memory stream.
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            //Go to the beginning of the memory stream.
            ms.Seek(0, SeekOrigin.Begin);

            //Fill the texture.
            texture = Texture2D.FromStream(graphics, ms, image.Width, image.Height, false);

            //Close the stream.
            ms.Close();
            ms = null;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XNAPopupWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "XNAPopupWindow";
            this.Load += new System.EventHandler(this.XNAPopupWindow_Load);
            this.ResumeLayout(false);

        }

        private void XNAPopupWindow_Load(object sender, EventArgs e)
        {

        }
    }
}