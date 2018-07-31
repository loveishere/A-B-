using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace hStore.Forms
{
    public delegate void CheckCellEventHandler(object sender, DataGridEnableEventArgs e);



    public class ColumnStyle : DataGridTextBoxColumn
    {
        //public event CheckCellEventHandler CheckCellEven;
        public event CheckCellEventHandler CheckCellContains;
        //public event CheckCellEventHandler CheckCellEquals;

        private int _col;

        public ColumnStyle(int column, string MappingName, string HeaderText, int Width, string NullText)
        {
            _col = column;
            this.MappingName = MappingName;
            this.HeaderText = HeaderText;
            this.Width = Width;
            this.NullText = NullText;
        }

        public ColumnStyle(int column, string MappingName, string HeaderText, int Width, string NullText, CheckCellEventHandler eventHandler)
        {
            _col = column;
            this.MappingName = MappingName;
            this.HeaderText = HeaderText;
            this.Width = Width;
            this.NullText = NullText;
            this.CheckCellContains += eventHandler;
        }


        //public ColumnStyle(int column)
        //{
        //_col = column;
        //}

        protected override void Paint(Graphics g, Rectangle Bounds, CurrencyManager Source, int RowNum, Brush BackBrush, Brush ForeBrush, bool AlignToRight)
        {
            //can remove this if you don't want to vary the formatting on disabled cells
            bool enabled = true;

            //check if our event handler is not null for this cell and then signal the event
            //the returning result in 'e' will contain a true or false boolean value
            //the event can be 'receieved' anywhere in our program and the logic can be anything
            // we choose.  The method called from the event simply has to match our 'CheckCellEventHandler'
            //delegate.
            //if (CheckCellEven != null)
            //{
            //    DataGridEnableEventArgs e = new DataGridEnableEventArgs(RowNum, _col, enabled);
            //    CheckCellEven(this, e);
            //    if (e.MeetsCriteria)
            //        BackBrush = new SolidBrush(Color.Orange);
            //}

            //repeat again for our second event handler
            if (CheckCellContains != null)
            {
                DataGridEnableEventArgs e = new DataGridEnableEventArgs(RowNum, _col, enabled);
                CheckCellContains(this, e);
                if (e.MeetsCriteria)
                {
                    BackBrush = new SolidBrush(e.BackColor);
                    ForeBrush = new SolidBrush(e.ForeColor);
                }
            }

            base.Paint(g, Bounds, Source, RowNum, BackBrush, ForeBrush, AlignToRight);

            //out third event handler , we are going to use this draw a rectangle around our
            //cell so we call it after we have called the base class paint method
            //if (CheckCellEquals != null)
            //{
            //    DataGridEnableEventArgs e = new DataGridEnableEventArgs(RowNum, _col, enabled);
            //    CheckCellEquals(this, e);
            //    if (e.MeetsCriteria)
            //        g.DrawRectangle(new Pen(Color.Red, 2), Bounds.X + 1, Bounds.Y + 1, Bounds.Width - 2, Bounds.Height - 2);
            //}
        }

    }

    public class DataGridEnableEventArgs : EventArgs
    {
        private int _column;
        private int _row;
        private bool _meetsCriteria;
        private Color _backcolor;
        private Color _forecolor;

        public DataGridEnableEventArgs(int row, int col, bool val)
        {
            _row = row;
            _column = col;
            _meetsCriteria = val;
        }

        public int Column
        {
            get { return _column; }
            set { _column = value; }
        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public bool MeetsCriteria
        {
            get { return _meetsCriteria; }
            set { _meetsCriteria = value; }
        }
        public Color BackColor
        {
            get { return _backcolor; }
            set { _backcolor = value; }
        }
        public Color ForeColor
        {
            get { return _forecolor; }
            set { _forecolor = value; }
        }
    }
}
