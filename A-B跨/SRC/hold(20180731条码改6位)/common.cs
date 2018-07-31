using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace hStore
{
    public delegate void CheckCellEventHandler(object sender, DataGridEnableEventArgs e);

    public class ColumnStyle : DataGridTextBoxColumn
    {
        public event CheckCellEventHandler CheckCellContains;

        private int _col;

        public ColumnStyle(int column, string MappingName, string HeaderText, int Width, string NullText)
        {
            _col = column;
            this.MappingName = MappingName;
            this.HeaderText = HeaderText;
            this.Width = Width;
            this.NullText = NullText;
        }

        public ColumnStyle(int column, string MappingName, string HeaderText, int Width, string NullText,CheckCellEventHandler eventHandler)
        {
            _col = column;
            this.MappingName = MappingName;
            this.HeaderText = HeaderText;
            this.Width = Width;
            this.NullText = NullText;
            this.CheckCellContains += eventHandler;
        }

        public ColumnStyle(int column, string MappingName, string HeaderText, int Width, CheckCellEventHandler eventHandler)
        {
            _col = column;
            this.MappingName = MappingName;
            this.HeaderText = HeaderText;
            this.Width = Width;
            this.CheckCellContains += eventHandler;
        }

        protected override void Paint(Graphics g, Rectangle Bounds, CurrencyManager Source, int RowNum, Brush BackBrush, Brush ForeBrush, bool AlignToRight)
        {
            bool enabled = true;

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
