using System;
using System.Collections.Generic;
using System.Text;

namespace Datenserver
{
    class Daten
    {
        private int _temperatur;
        private int _luftfeuchtigkeit;
        private int _wetter;
        private DateTime _erhalten;
        public int Temperatur => _temperatur;
        public int Luftfeuchtigkeit => _luftfeuchtigkeit;
        public int Wetter => _wetter;
        public DateTime Erhalten => _erhalten;
        public Daten(int luftfeuchtigkeit, int temperatur, int wetter)
        {
            _temperatur = temperatur;
            _luftfeuchtigkeit = luftfeuchtigkeit;
            _wetter = wetter;
            _erhalten = DateTime.Now;
        }
        public String printOut()
        {
            return _luftfeuchtigkeit + ";" + _temperatur + ";" + _wetter+ ";" + _erhalten;
        }
    }
}
