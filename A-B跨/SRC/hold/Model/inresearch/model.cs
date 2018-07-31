using System;
using System.Collections.Generic;
using System.Text;

namespace hStore.Model.InResearch
{
    public class inplan
    {
        private string _jhh;
        private double? _jz;
        private double? _mz;
        private int? _js;
        private int? _wcjs;

        public string jhh
        {
            set { _jhh = value; }
            get { return _jhh; }
        }
        public double? jz
        {
            set { _jz = value; }
            get { return _jz; }
        }

        public double? mz
        {
            set { _mz = value; }
            get { return _mz; }
        }

        public int? js
        {
            set { _js = value; }
            get { return _js; }
        }

        public int? wcjs
        {
            set { _wcjs = value; }
            get { return _wcjs; }
        }

        public inplan(string jhh, double? jz, double? mz, int? js,int? wcjs)
        {
            _jhh = jhh;
            _jz = jz;
            _mz = mz;
            _js = js;
            _wcjs = wcjs;
        }
    }

    public class inplanzf
    {
        private string _zfh;
        private string _pm;
        private string _hth;
        private double? _jz;
        private double? _mz;
        private int? _js;
        private int? _wcjs;

        public string zfh
        {
            set { _zfh = value; }
            get { return _zfh; }
        }
        public string pm
        {
            set { _pm = value; }
            get { return _pm; }
        }
        public string hth
        {
            set { _hth = value; }
            get { return _hth; }
        }

        public double? jz
        {
            set { _jz = value; }
            get { return _jz; }
        }

        public double? mz
        {
            set { _mz = value; }
            get { return _mz; }
        }

        public int? js
        {
            set { _js = value; }
            get { return _js; }
        }

        public int? wcjs
        {
            set { _wcjs = value; }
            get { return _wcjs; }
        }

        public inplanzf(string zfh,string pm,string hth, double? jz, double? mz, int? js, int? wcjs)
        {
            _zfh = zfh;
            _pm = pm;
            _hth = hth;
            _jz = jz;
            _mz = mz;
            _js = js;
            _wcjs = wcjs;
        }
    }

    public class inplancl
    {
        private string _clh;
        private string _kw;
        private double? _mz;
        private double? _jz;
        private string _gg;

        public string clh
        {
            set { _clh = value; }
            get { return _clh; }
        }
        public string kw
        {
            set { _kw = value; }
            get { return _kw; }
        }
        public double? mz
        {
            set { _mz = value; }
            get { return _mz; }
        }

        public double? jz
        {
            set { _jz = value; }
            get { return _jz; }
        }
        public string gg
        {
            set { _gg = value; }
            get { return _gg; }
        }
        public inplancl(string clh,string kw, double? jz, double? mz, string gg)
        {
            _clh = clh;
            _kw = kw;
            _jz = jz;
            _mz = mz;
            _gg = gg;
        }
    }

    public class inwarehouse
    {
        private string _clh;
        private string _kw;
        private double? _mz;

        public string clh
        {
            set { _clh = value; }
            get { return _clh; }
        }
        public string kw
        {
            set { _kw = value; }
            get { return _kw; }
        }
        public double? mz
        {
            set { _mz = value; }
            get { return _mz; }
        }

        public inwarehouse(string clh, string kw, double? mz)
        {
            _clh = clh;
            _kw = kw;
            _mz = mz;
        }
    }
}
