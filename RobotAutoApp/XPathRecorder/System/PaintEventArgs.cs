namespace System
{
    internal class PaintEventArgs
    {
        private Action<object, Windows.Forms.PaintEventArgs> btnSync_Click;

        public PaintEventArgs(Action<object, Windows.Forms.PaintEventArgs> btnSync_Click)
        {
            this.btnSync_Click = btnSync_Click;
        }
    }
}