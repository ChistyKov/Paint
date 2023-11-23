using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.model
{
    public static class OblickCreator
    {
        public static Oblick Creator(AllOblicks Oblicks)
        {

            if (Oblicks == AllOblicks.Circle)
                return new Circle();
            if(Oblicks == AllOblicks.Line)
                return new Line();
            if (Oblicks == AllOblicks.Rectangle)
                return new Rectangl();
            if (Oblicks == AllOblicks.Oval)
                return new Oval();
            else return new Line();
        }
    }
}
