using System;
using System.Collections.Generic;
using System.Text;

namespace IUT_Vache
{


    public class Piquet
    {

        private static readonly List<Piquet> liste_piquet = new List<Piquet>();
        //private static readonly List<double> segments = new List<double>();

        private readonly double x;
        private readonly double y;

        /// <summary>
        /// Objet Piquet, crée un piquet pour la clôture
        /// </summary>
        /// <param name="x">Coordonnée x du piquet</param>
        /// <param name="y">Coordonnée y du piquet</param>
        public Piquet(double x, double y)
        {
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
        /// <summary>
        /// Calcul de l'aire de la clôture en fonction des données rentrées
        /// </summary>
        /// <returns>L'aire de la clôture</returns>

        public static double GetAire()
        {
            double aire = 0.0;

            for (int i = 0; i < (liste_piquet.Count) - 1; i++)
            {
                Piquet piquet = liste_piquet[i];
                double coordX = piquet.x;
                double coordY = piquet.y;
                double coordXPlusUn = liste_piquet[i + 1].x;
                double coordYPlusUn = liste_piquet[i + 1].y;

                //Calcul du segment
                double segment =
                    (coordX * coordYPlusUn) - (coordXPlusUn * coordY);

                //Si on arrive au dernier segment (fusion avec le segment 0)
                if (i == liste_piquet.Count - 2)
                {
                    double segmentNmoins1 =
                        (liste_piquet[i + 1].x * liste_piquet[0].y) - (liste_piquet[0].x * liste_piquet[i + 1].y);

                    aire += segment + segmentNmoins1;
                }
                else
                {
                    aire += segment;
                }
            }

            return Math.Round(Math.Abs(aire * 0.5), 3, MidpointRounding.ToEven);
        }

        public static double GetCentreGraviteX()
        {
            double gravityX = 0.0;

            for (int i = 0; i < (liste_piquet.Count) - 1; i++)
            {
                Piquet piquet = liste_piquet[i];
                double coordX = piquet.x;
                double coordY = piquet.y;
                double coordXPlusUn = liste_piquet[i + 1].x;
                double coordYPlusUn = liste_piquet[i + 1].y;

                //Application de la formule (de la somme)
                double somme = (coordX + coordXPlusUn) *
                    (coordX * coordYPlusUn - coordXPlusUn * coordY);

                //Si on arrive au dernier segment (fusion avec le segment 0)
                if (i == liste_piquet.Count - 2)
                {
                    double segmentNmoins1 = (liste_piquet[i + 1].x + liste_piquet[0].x) *
                        (liste_piquet[i + 1].x * liste_piquet[0].y - liste_piquet[0].x * liste_piquet[i + 1].y);

                    gravityX += somme + segmentNmoins1;
                }
                else
                {
                    gravityX += somme;
                }
            }

            return Math.Round((1 / (6 * GetAire())) * gravityX, 3, MidpointRounding.ToEven);
        }

        public static double GetCentreGraviteY()
        {
            double gravityY = 0.0;

            for (int i = 0; i < (liste_piquet.Count) - 1; i++)
            {
                Piquet piquet = liste_piquet[i];
                double coordX = piquet.x;
                double coordY = piquet.y;
                double coordXPlusUn = liste_piquet[i + 1].x;
                double coordYPlusUn = liste_piquet[i + 1].y;

                //Application de la formule (de la somme)
                double somme = (coordY + coordYPlusUn) *
                    (coordX * coordYPlusUn - coordXPlusUn * coordY);

                //Si on arrive au dernier segment (fusion avec le segment 0)
                if (i == liste_piquet.Count - 2)
                {
                    double segmentNmoins1 = (liste_piquet[i + 1].y + liste_piquet[0].y) *
                        (liste_piquet[i + 1].x * liste_piquet[0].y - liste_piquet[0].x * liste_piquet[i + 1].y);

                    gravityY += somme + segmentNmoins1;
                }
                else
                {
                    gravityY += somme;
                }
            }

            return Math.Round((1 / (6 * GetAire())) * gravityY, 3, MidpointRounding.ToEven);
        }
        /// <summary>
        /// Calcul de l'ordonnée ou l'abscisse du centre de gravité en fonction du booléen "axe"
        /// </summary>
        /// <returns>L'ordonnée ou l'abscisse du centre de gravité.</returns>
        /// <param name="axe">Boolean pour l'axe choisi TRUE = abscisse | FALSE = ordonnée </param>
        public static double GetCentreGravite(bool axe)
        {
            double gravity = 0.0;

            for (int i = 0; i < (liste_piquet.Count) - 1; i++)
            {
                Piquet piquet = liste_piquet[i];
                double coordX = piquet.x;
                double coordY = piquet.y;
                double coordXPlusUn = liste_piquet[i + 1].x;
                double coordYPlusUn = liste_piquet[i + 1].y;
                double somme;

                //Application de la formule (de la somme)
                if (axe)
                {
                    somme = (coordX + coordXPlusUn);
                } else
                {
                    somme = (coordY + coordYPlusUn);
                }

                somme *= (coordX * coordYPlusUn - coordXPlusUn * coordY);

                //Si on arrive au dernier segment (fusion avec le segment 0)
                if (i == liste_piquet.Count - 2)
                {
                    double segmentNmoins1;
                    if (axe)
                    {
                        segmentNmoins1 = (liste_piquet[i + 1].x + liste_piquet[0].x);
                    } else
                    {
                        segmentNmoins1 = (liste_piquet[i + 1].y + liste_piquet[0].y);
                    }
                    segmentNmoins1 *= 
                        (liste_piquet[i + 1].x * liste_piquet[0].y - liste_piquet[0].x * liste_piquet[i + 1].y);

                    gravity += somme + segmentNmoins1;
                }
                else
                {
                    gravity += somme;
                }
            }

            return Math.Round((1 / (6 * GetAire())) * gravity, 3, MidpointRounding.ToEven);
        }

    }
}
