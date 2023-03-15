using Ozeki.Camera;
using Ozeki.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sojro.Myforms
{
    public partial class Dermascope : Form
    {
        private OzekiCamera _camera;
        private DrawingImageProvider _Provider;
        private MediaConnector _connector;
        private Zoom zoom;
        private IVideoSender videoSender;
        private MPEG4Recorder recorder;
        private SnapshotHandler snapshotHandler;

        private ToolTip _toolTip = new ToolTip();
        //private Control _currentToolTipControl = null;
        string recordBtnToolTip = "Record";
        string stopdBtnToolTip = "Disabled";
        string snapshotdBtnToolTip = "Take Snapshot";
        string savedBtnToolTip = "Saving to ";
        bool recording = false;

        string globalCamera = "";

        public Dermascope(string _vame)
        {
            InitializeComponent();
            _connector = new MediaConnector();
            _Provider = new DrawingImageProvider();
            videoViewerWF1.SetImageProvider(_Provider);
            zoom = new Zoom();
            snapshotHandler = new SnapshotHandler();
            globalCamera = _vame;
            _toolTip.SetToolTip(this.guna2CircleButton1, recordBtnToolTip);
            _toolTip.SetToolTip(this.guna2CircleButton2, stopdBtnToolTip);
            _toolTip.SetToolTip(this.guna2Button1, snapshotdBtnToolTip);
            folderBrowserDialog1.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            _toolTip.SetToolTip(this.guna2Button2, savedBtnToolTip + folderBrowserDialog1.SelectedPath);
        }

        private void videoViewerWF1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                recording = true;
                guna2CircleButton1.BackgroundImage = global::Sojro.Properties.Resources.recording1;
                recordBtnToolTip = "Recording";
                stopdBtnToolTip = "Stop Recording";
                guna2CircleButton2.Enabled = true;

                _toolTip.SetToolTip(this.guna2CircleButton1, recordBtnToolTip);
                _toolTip.SetToolTip(this.guna2CircleButton2, stopdBtnToolTip);
            }

            if (videoSender == null)
            {
                return;
            }
            var datea = DateTime.Now.DayOfWeek + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "(" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ")";
            var currentPAth = folderBrowserDialog1.SelectedPath + "\\" + datea + ".mp4";

            recorder = new MPEG4Recorder(currentPAth);
            recorder.MultiplexFinished += recorder_Multiplexfinished;

            _connector.Connect(videoSender, recorder.VideoRecorder);
            
        }
        void recorder_Multiplexfinished(Object sender, VoIPEventArgs<bool> e)
        {
            recorder.MultiplexFinished -= recorder_Multiplexfinished;
            recorder.Dispose();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (recording)
            {
                recording = false;
                guna2CircleButton1.BackgroundImage = global::Sojro.Properties.Resources.record;
                recordBtnToolTip = "Record";
                stopdBtnToolTip = "Disabled";
                guna2CircleButton2.Enabled = false;

                _toolTip.SetToolTip(this.guna2CircleButton1, recordBtnToolTip);
                _toolTip.SetToolTip(this.guna2CircleButton2, stopdBtnToolTip);
            }
            //Stop
            if (videoSender == null)
            {
                return;
            }
            _connector.Disconnect(videoSender, recorder.VideoRecorder);
            recorder.Multiplex();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //SnapSHot
            var datea = DateTime.Now.DayOfWeek + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "(" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ")";

            var currentPAth = folderBrowserDialog1.SelectedPath + "\\" + datea + ".jpg";

            var img = snapshotHandler.TakeSnapshot().ToImage() as System.Drawing.Image;
            img.Save(currentPAth);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var _path = folderBrowserDialog1.SelectedPath + "\\";
                //MessageBox.Show(_path);
            }
            _toolTip.SetToolTip(this.guna2Button2, savedBtnToolTip + folderBrowserDialog1.SelectedPath);
        }

        private void Dermascope_Load(object sender, EventArgs e)
        {
            _camera = new OzekiCamera(globalCamera);
            //_camera.CameraStateChanged += _camera_CameraStateChanged;
            _connector.Connect(_camera.VideoChannel, zoom);
            _connector.Connect(zoom, _Provider);

            //For Recording and SnapShot

            _connector.Connect(zoom, snapshotHandler);

            videoSender = zoom;     //zoom for recording with zoom feature, otherwise _camera.VideoChannel

            _camera.Start();
            videoViewerWF1.Start();
            zoom.Start();
        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
