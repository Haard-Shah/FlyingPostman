using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingPostMan
{
    /// <summary>
    /// Chromosome is the object that represents the DNA of the population. In current context, it represent a tour and it respective total length and fitnessScore.
    /// </summary>
    class Chromosome : IComparable 
    {
        public int[] tour;
        public double totalLen;
        public double fitnessScore;

        //Function to get a random number as running a tight loop in c# resutls in non random numbers due to c#'s implementation of using time as a seed for random number generation------

        // To avoid returning the same number again in random number generation syncLock object has been created which will be locked during synchronisation forcing a sinlge seed and hence every call to random.Next will enforce next number on rand gen to take place. 
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }// End RandomNumber 

        // ---------------------------------------------------------------------------

        /// <summary>
        /// Chromosome() initializer method, generate a randomised tour as part of the initialization. Utilized for initial generation creation and when more exploration of tours needs to occur in the algorithm.
        /// </summary>
        /// <param name="numStations"></param>
        public Chromosome(int numStations)
        {
            tour = LinSpace(numStations);


            for (int i = 0; i < (numStations); ++i)
            {
                // Making use of Swap method already written in class1 to avoid replicating code
                Class1.Swap(ref tour, RandomNumber(0, numStations), RandomNumber(0, numStations));
            }
        }// End chromosome()

        /// <summary>
        /// Overload of Chromosome() initializer, simply creates a chromosome object and assigns passed parameters. Mainly utlized to intilizer temporary place holder chromosomes.
        /// </summary>
        /// <param name="numOfStation"></param>
        /// <param name="fitnessScore"></param>
        public Chromosome(int numOfStation, double fitnessScore)
        {
            // Method to create the Fittest chromosome
            tour = new int[numOfStation];
            this.totalLen = fitnessScore;
        }//End Chromosome()

        /// <summary>
        /// overlaod of Chromosome(). Essientially, a alone method as classes can only inherit from one class as a time. Chromosome already inherits from IComparable so it can't inherit from ICloneable, Hence a Clone Method as a wrapper method.
        /// </summary>
        /// <param name="tour"></param>
        /// <param name="fitnessScore"></param>
        public Chromosome(int[] tour, double fitnessScore)
        {
            this.tour = tour;
            this.totalLen = fitnessScore;
        }// End Chromosome()

        /// <summary>
        /// CalcFittness() a wrapper method to calculate fitness of the chromosome based on the passed stations. NOTE: This method was planned to incorporate more functinality but wasn't able to due to time constraints.
        /// </summary>
        /// <param name="stations"></param>
        public void CalcFittness(List<Station> stations)
        {
            CalcDistBetweenAll(stations);
        }//End CalcFittness()

        /// <summary>
        /// CalcDistBetweenAll() adaptaion of original CalcDistBetweenAll() method in Class1 class (program.cs). Adaptopted to compute total length of a tour for a chromosome.
        /// </summary>
        /// <param name="stations"></param>
        private void CalcDistBetweenAll(List<Station> stations)
        {
            totalLen = Distance(stations[0], stations[tour[0]]);
            for (int i = 1; i < tour.Length; i++)
            {
                totalLen += Distance(stations[tour[i - 1]], stations[tour[i]]);
            }
            totalLen += Distance(stations[tour[tour.Length - 1]], stations[0]);
        } // End CalcDistBetweenAll

        /// <summary>
        /// Distance() method computes Equalidian Distance using c^2 = a^2 + b^2
        /// </summary>
        /// <param name="stationA"></param>
        /// <param name="stationB"></param>
        /// <returns></returns>
        public double Distance(Station stationA, Station stationB)
        {
            double diffX = stationA.X - stationB.X;
            double diffY = stationA.Y - stationB.Y;
            return Math.Sqrt((diffX * diffX) + (diffY * diffY)); //calculating Equalidian Distance using c^2 = a^2 + b^2
        }

        /// <summary>
        /// CompareTo() IComparable override of CompareTo(), adapted to compare two chromosomes based on their fitness level.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            // Each Chromosome must be compared to other chromosome based on it's fitness Score. This makes sorting array easier as well as the whole generation can now be sorted to pick the top group of fittest Chromosomes 

            Chromosome tourB = (Chromosome)obj;

            if (this.totalLen < tourB.totalLen) { return -1; }
            else if (this.totalLen > tourB.totalLen) { return 1; }
            else { return 0; }
        }// End CompareTo()

        /// <summary>
        /// ToString() override method, to represent a chromosomes in an understandable way; using it tour property.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";
            foreach (int station in tour)
            {
                str += Convert.ToString(station) + " ";
            }
            return str;
        }// End ToString()

        /// <summary>
        /// LinSpace() method Creates a linearly Spaced array starting from one to the Max value provided as an argument. It returns this linearly spaced array back as an int[].
        /// </summary>
        /// <param name="Max"></param>
        /// <returns></returns>
        private int[] LinSpace(int Max)
        {
            // linSpace Creates a linearly Spaced array starting from one to the Max value provided to the method

            int[] linSpace = new int[Max];
            for (int i = 0; i < Max; ++i)
            {
                linSpace[i] = i + 1;
            }

            return linSpace;
        }// End LinSpace

    }// End Chromosome Class

}// End FlyingPostMan namespace
