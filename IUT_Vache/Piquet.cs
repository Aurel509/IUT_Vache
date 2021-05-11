using System;
using System.Collections.Generic;
using System.Text;

namespace IUT_Vache
{


    public class Piquet
    {

        private static List<Piquet> liste_piquet = new List<Piquet>();

        private int id;
        private double x;
        private double y;


        public Piquet(double x, double y)
        {
            id = liste_piquet.Count + 1;
            this.x = x;
            this.y = y;
            liste_piquet.Add(this);
        }

        public static List<Piquet> GetList()
        {
            return liste_piquet;
        }
        public static int GetListCount()
        {
            return liste_piquet.Count;
        }
        public static double GetAire()
        {
            double aire = 0.0;

            foreach(Piquet piquet in liste_piquet)
            {
                //
            }

            return aire;
        }
    }
}
