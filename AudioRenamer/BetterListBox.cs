// Based on nobugz post here: https://social.msdn.microsoft.com/Forums/en-US/46d8cba4-1266-4f39-a27b-5e86a4cf3583/listbox-verticle-scroll-bar-event?forum=Vsexpressvcs
// Modified to support the mouse wheel as well

using System;
using System.Windows.Forms;

public class BetterListBox : ListBox
{
    // Event declaration
    public delegate void BetterListBoxScrollDelegate(object Sender, BetterListBoxScrollArgs e);
    public event BetterListBoxScrollDelegate Scroll;
    // WM_VSCROLL message constants
    private const int WM_VSCROLL = 0x115;
    private const int WM_MOUSEWHEEL = 0x20A;
    private const int SB_THUMBTRACK = 5;
    private const int SB_ENDSCROLL = 8;

    protected override void WndProc(ref Message m)
    {
        // Trap the WM_VSCROLL message to generate the Scroll event
        base.WndProc(ref m);
        if (m.Msg == WM_VSCROLL)
        {
            int nfy = m.WParam.ToInt32() & 0xFFFF;
            if (Scroll != null && (nfy == SB_THUMBTRACK || nfy == SB_ENDSCROLL))
                Scroll(this, new BetterListBoxScrollArgs(this.TopIndex, nfy == SB_THUMBTRACK));
        }
        else if (m.Msg == WM_MOUSEWHEEL)
        {
            if (Scroll != null)
            {
                Scroll(this, new BetterListBoxScrollArgs(this.TopIndex, false));
            }
        }
    }
    public class BetterListBoxScrollArgs
    {
        // Scroll event argument
        private int mTop;
        private bool mTracking;
        public BetterListBoxScrollArgs(int top, bool tracking)
        {
            mTop = top;
            mTracking = tracking;
        }
        public int Top
        {
            get { return mTop; }
        }
        public bool Tracking
        {
            get { return mTracking; }
        }
    }
}
