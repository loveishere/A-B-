using System;
using System.Collections.Generic;
using System.Text;

namespace hStore.Model.OutResearch
{
    public class outplan
    {
        private string _jhh;
        private string _jhdd;
        private string _shdw;
        private double? _jz;
        private double? _mz;
        private int? _js;
        private int? _wcjs;

        public string jhh
        {
            set { _jhh = value; }
            get { return _jhh; }
        }
        public string jhdd
        {
            set { _jhdd = value; }
            get { return _jhdd; }
        }
        public string shdw
        {
            set { _shdw = value; }
            get { return _shdw; }
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

        public outplan(string jhh,string jhdd,string shdw, double? jz, double? mz, int? js, int? wcjs)
        {
            _jhh = jhh;
            _jhdd = jhdd;
            _shdw = shdw;
            _jz = jz;
            _mz = mz;
            _js = js;
            _wcjs = wcjs;
        }
    }

    public class outplan2
    {
        private string _jhh;
        private string _kw;
        private string _jhdd;
        private string _shdw;
        private double? _jz;
        private double? _mz;
        private string _gdh;
        private int? _js;
        private int? _wcjs;

        public string jhh
        {
            set { _jhh = value; }
            get { return _jhh; }
        }
        public string kw
        {
            set { _kw = value; }
            get { return _kw; }
        }
        public string jhdd
        {
            set { _jhdd = value; }
            get { return _jhdd; }
        }
        public string shdw
        {
            set { _shdw = value; }
            get { return _shdw; }
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
        public string gdh
        {
            set { _gdh = value; }
            get { return _gdh; }
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

        public outplan2(string jhh,string kw, string jhdd, string shdw, double? jz, double? mz, string gdh, int? js, int? wcjs)
        {
            _jhh = jhh;
            _kw = kw;
            _jhdd = jhdd;
            _shdw = shdw;
            _jz = jz;
            _mz = mz;
            _gdh = gdh;
            _js = js;
            _wcjs = wcjs;
        }
    }

    public class outplan3
    {
        private string _jhh;
        private string _zph;
        private string _jhdd;
        private string _shdw;
        private double? _jz;
        private double? _mz;
        private int? _js;
        private int? _wcjs;

        public string jhh
        {
            set { _jhh = value; }
            get { return _jhh; }
        }
        public string zph
        {
            set { _zph = value; }
            get { return _zph; }
        }
        public string jhdd
        {
            set { _jhdd = value; }
            get { return _jhdd; }
        }
        public string shdw
        {
            set { _shdw = value; }
            get { return _shdw; }
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

        public outplan3(string jhh, string zph, string jhdd, string shdw, double? jz, double? mz, int? js, int? wcjs)
        {
            _jhh = jhh;
            _zph = zph;
            _jhdd = jhdd;
            _shdw = shdw;
            _jz = jz;
            _mz = mz;
            _js = js;
            _wcjs = wcjs;
        }
    }

    public class outplanzf
    {
        private string _zfh;
        private double? _jz;
        private double? _mz;
        private int? _js;
        private int? _wcjs;
        private string _pm;
        private string _hth;

        public string zfh
        {
            set { _zfh = value; }
            get { return _zfh; }
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

        public outplanzf(string zfh,  double? jz, double? mz, int? js, int? wcjs,string pm, string hth)
        {
            _zfh = zfh;
            _jz = jz;
            _mz = mz;
            _js = js;
            _wcjs = wcjs;
            _pm = pm;
            _hth = hth;
        }
    }

    public class outplancl
    {
        private string _clh;
        private string _kw;
        private double? _jz;
        private double? _mz;
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

        public string gg
        {
            set { _gg = value; }
            get { return _gg; }
        }


        public outplancl(string clh, string kw, double? jz, double? mz, string gg)
        {
            _clh = clh;
            _kw = kw;
            _jz = jz;
            _mz = mz;
            _gg = gg;
        }
    }

    public class outwarehousezc
    {
        private string _clh;
        private string _kw;
        private string _gd;
        private double? _mz;
        private string _dd;

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
        public string gd
        {
            set { _gd = value; }
            get { return _gd; }
        }
        public double? mz
        {
            set { _mz = value; }
            get { return _mz; }
        }

        public string dd
        {
            set { _dd = value;}
            get { return _dd; }
        }

        public outwarehousezc(string clh,string kw,string gd, double? mz,string dd)
        {
            _clh = clh;
            _kw = kw;
            _gd = gd;
            _mz = mz;
            _dd = dd;
        }
    }

    public class outwarehousetl
    {
        private string _clh;
        private double? _mz;
        private string _zph;

        public string clh
        {
            set { _clh = value; }
            get { return _clh; }
        }
        public double? mz
        {
            set { _mz = value; }
            get { return _mz; }
        }
        public string zph
        {
            set { _zph = value; }
            get { return _zph; }
        }

        public outwarehousetl(string clh, double? mz,string zph)
        {
            _clh = clh;
            _mz = mz;
            _zph = zph;
        }
    }

    public class outwarehousezk
    {
        private string _clh;
        private string _dd;
        private double? _mz;

        public string clh
        {
            set { _clh = value; }
            get { return _clh; }
        }
        public string dd
        {
            set { _dd = value; }
            get { return _dd; }
        }

        public double? mz
        {
            set { _mz = value; }
            get { return _mz; }
        }

        public outwarehousezk(string clh, string dd, double? mz)
        {
            _clh = clh;
            _dd = dd;
            _mz = mz;
        }
    }

    public class outwarehousezt
    {
        private string _clh;
        private double? _mz;
        private string _kw;

        public string clh
        {
            set { _clh = value; }
            get { return _clh; }
        }

        public double? mz
        {
            set { _mz = value; }
            get { return _mz; }
        }
        public string kw
        {
            set { _kw = value; }
            get { return _kw; }
        }

        public outwarehousezt(string clh,double? mz,string kw)
        {
            _clh = clh;
            _mz = mz;
            _kw = kw;
        }
    }

    public class outwarehousetl2
    {
        private string _clh;
        private string _kw;
        private double? _mz;
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
        public string gg
        {
            set { _gg = value; }
            get { return _gg; }
        }

        public outwarehousetl2(string clh, string kw, double? mz, string gg)
        {
            _clh = clh;
            _kw = kw;
            _mz = mz;
            _gg = gg;
        }
    }

    public class shipselect
    {
        private string _cph;
        private string _cm;
        private int? _zjs;
        private int? _wcjs;

        public string cph
        {
            set { _cph = value; }
            get { return _cph; }
        }
        public string cm
        {
            set { _cm = value; }
            get { return _cm; }
        }

        public int? zjs
        {
            set { _zjs = value; }
            get { return _zjs; }
        }

        public int? wcjs
        {
            set { _wcjs = value; }
            get { return _wcjs; }
        }

        public shipselect(string cph, string cm, int? zjs,int? wcjs)
        {
            _cph = cph;
            _cm = cm;
            _zjs = zjs;
            _wcjs = wcjs;
        }
    }

    public class zphall
    {
        private string _zph;
        private int? _zjs;
        private int? _wcjs;

        public string zph
        {
            set { _zph = value; }
            get { return _zph; }
        }
        public int? zjs
        {
            set { _zjs = value; }
            get { return _zjs; }
        }

        public int? wcjs
        {
            set { _wcjs = value; }
            get { return _wcjs; }
        }

        public zphall(string zph, int? zjs, int? wcjs)
        {
            _zph = zph;
            _zjs = zjs;
            _wcjs = wcjs;
        }
    }
}
