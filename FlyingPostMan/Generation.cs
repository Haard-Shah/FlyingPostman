using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingPostMan
{
    /// <summary>
    /// Generation represents a Generation object which represents a population of chromosomes and an overall generation fitness value.
    /// </summary>
    class Generation
    {
        public Chromosome[] Population;
        public double GenFitness;

        /// <summary>
        /// Generation() initializer method, generates a new Generation of Chromosomes.
        /// </summary>
        /// <param name="popSize"></param>
        /// <param name="stations"></param>
        public Generation(int popSize, List<Station> stations)
        {
            //Initialise the population of the generaiton 
            Population = new Chromosome[popSize];            
        }

        /// <summary>
        ///  Overload of Generation() with extension of fittest Chromosome from previous Generation as first member of Next Generation
        /// </summary>
        /// <param name="popSize"></param>
        /// <param name="fittest"></param>
        public Generation(int popSize, Chromosome fittest)
        {
            //Initialise the population of the generaiton 
            Population = new Chromosome[popSize];

            Population[0] = fittest; //Keep the fittest from Previous Gen
        } // End Generation


        /// <summary>
        /// findFittest() finds the fittest chromosome of the generation. Using the list of stations to calculate each chromosome's fittness level.
        /// </summary>
        /// <param name="stations"></param>
        /// <returns></returns>
        public Chromosome findFittest(List<Station> stations)
        {
            // Variables
            int numOfStations = stations.Count - 1;
            string temp = ""; // REMOVE

            Chromosome Fittest = new Chromosome(numOfStations, double.PositiveInfinity);


            for (int i = 0; i < Population.Length; ++i)
            {
                Population[i] = new Chromosome(numOfStations);
                //Console.WriteLine(Gen0.Population[i]); //REMOVE 
                Population[i].CalcFittness(stations);
                Population[i].fitnessScore = 1 / Population[i].totalLen;
                GenFitness += Population[i].fitnessScore;
                //Console.WriteLine(Gen0.Population[i].fitnessScore + "\n");
            }

            NormaliseFitness();

            // Sort the Population on Fitness of the population
            Array.Sort(Population); 

            Fittest = Population[0];

            Console.WriteLine(Fittest.totalLen);
            return Fittest;

        } // End FindFittest

        /// <summary>
        /// NormaliseFitness() normalises fitness of all the whole population of chromosomes based on the overall Generation Fitness to give all the members a relative fitness to each other.
        /// </summary>
        private void NormaliseFitness()
        {
            // Normalise each of the Chromosome based on the Total Fitness of the population
            foreach(Chromosome chromosome in Population)
            {
                chromosome.fitnessScore /= GenFitness;
            }
            
        }// End NormaliseFitness()

    }// End Generation class

}// End FlyingPostMan namespace
