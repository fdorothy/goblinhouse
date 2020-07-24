using System;

namespace Retroverse
{
    [System.Serializable]
    public class Line
    {
        public string actor;
        public string text;

        public Line(string text = "", string actor = "main")
        {
            this.text = text;
            this.actor = actor;
        }
    }
}
