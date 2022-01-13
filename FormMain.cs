using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace DisableFadein
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 無音の音源
        /// </summary>
        private Stream silentAudioStream { get; } = Properties.Resources.silent;

        /// <summary>
        /// 音源流すやつ
        /// </summary>
        private SoundPlayer player;

        public FormMain()
        {
            InitializeComponent();
            player = new SoundPlayer(silentAudioStream);

            StartPlaying();
        }

        private void StartMenu_Clicked(object sender, EventArgs e) => StartPlaying();
        private void ExitMenu_Clicked(object sender, EventArgs e) => ExitApplication();
        private void PauseMenu_Clicked(object sender, EventArgs e) => PausePlaying();

        /// <summary>
        /// 無音ファイル再生状態にします
        /// </summary>
        private void StartPlaying()
        {
            if (player == null)
            {
                ExitApplication();
            }
            player.Stop();
            player.PlayLooping();

            StartToolStripMenuItem.Enabled = false;
            PauseToolStripMenuItem.Enabled = true;
            notifyIcon1.Text = "DisableFadein" + "(動作中)";
        }

        /// <summary>
        /// 無音ファイル一時停止状態にします
        /// </summary>
        private void PausePlaying()
        {
            if (player == null)
            {
                ExitApplication();
            }
            player.Stop();
            StartToolStripMenuItem.Enabled = true;
            PauseToolStripMenuItem.Enabled = false;
            notifyIcon1.Text = "DisableFadein" + "(停止中)";
        }

        /// <summary>
        /// アプリを終了します
        /// </summary>
        private void ExitApplication() => Application.Exit();

        /// <summary>
        /// FormMain.FormClosingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //アイコン消してからExitしないとタスクバーにゴミが残る
            notifyIcon1.Visible = false;
            Invalidate();

            //音源止める
            player?.Stop();
            player?.Dispose();
            player = null;
        }
    }
}
