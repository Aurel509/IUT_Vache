using System;
using System.Collections.Generic;
using System.Text;

namespace IUT_Vache
{


    public class Piquet
    {

        private static readonly List<Piquet> liste_piquet = new List<Piquet>();

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
                //Déclaration de variables
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
                //Déclaration de variables
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
            //Calcul du début de somme
            return Math.Round((1 / (6 * GetAire())) * gravity, 3, MidpointRounding.ToEven);
        }

        /// <summary>
        /// Fonction de calcul de l'angle théta
        /// </summary>
        /// <returns>L'Angle théta</returns>
        /// <param name="sommet">Liste des deux sommets</param>
        /// <param name="gravitex">Abscisse du point G</param>
        /// <param name="gravitey">Ordonnée du point G</param>
        public static double CalculAngleTheta(List<Piquet> sommet, double gravitex, double gravitey)
        {
            //Déclaration de variables
            double angle;
            double coordXSommet = sommet[0].x;
            double coordYSommet = sommet[0].y;
            double coordXPlus1Sommet = sommet[1].x;
            double coordYPlus1Sommet = sommet[1].y;


            double vecteurGSiX = coordXSommet - gravitex;
            double vecteurGSiY = coordYSommet - gravitey;
            double vecteurGSiplus1X = coordXPlus1Sommet - gravitex;
            double vecteurGSiplus1Y = coordYPlus1Sommet - gravitey;

            //Calcul du produit scalaire (numérateur de la formule)
            double produitScalaire = vecteurGSiX * vecteurGSiplus1X + vecteurGSiY * vecteurGSiplus1Y;

            //Normes des deux vecteurs (dénominateur de la formule)
            double normeVecteurGSi = Math.Sqrt(Math.Pow(vecteurGSiX,2) + Math.Pow(vecteurGSiY,2));
            double normeVecteurGSiplus1 = Math.Sqrt(Math.Pow(vecteurGSiplus1X, 2) + Math.Pow(vecteurGSiplus1Y, 2));


            angle = Math.Acos((produitScalaire) / (normeVecteurGSi * normeVecteurGSiplus1));

            //Calcul déterminant pour le signe de angle.
            if ((vecteurGSiX * vecteurGSiplus1X + vecteurGSiY * vecteurGSiplus1Y) < 0)
            {
                angle = -angle;
            }

            return angle;
        }

        /// <summary>
        /// Fonction du calcul de l'appartenance d'un sur un polygone
        /// </summary>
        /// <returns>Si le point est sur le polygone donnée ou non</returns>
        /// <param name="sommet">Liste des deux sommets</param>
        /// <param name="gravitex">Abscisse du point G</param>
        /// <param name="gravitey">Ordonnée du point G</param>
        public static bool AppartenancePointPolygone(List<Piquet> poly, double gravitex, double gravitey)
        {
            double somme = 0.0;
            double thetai;
            for(int i = 0; i < poly.Count - 1; i++)
            {
                //On envoi les sommets pour le calcul de angle

                List<Piquet> sommet = new List<Piquet>();

                if(i == poly.Count - 2)
                {
                    sommet.Add(poly[i+1]);
                    sommet.Add(poly[0]);
                } else
                {
                    sommet.Add(poly[i]);
                    sommet.Add(poly[i + 1]);
                }


                thetai = CalculAngleTheta(sommet, gravitex, gravitey);
                somme += thetai;
                sommet.Clear();
            }

            if(somme == 0)
            {
                return false;
            } else
            {
                return true;
            }
        }


    }
}
