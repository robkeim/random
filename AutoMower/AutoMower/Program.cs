namespace MowerSimulator
{
    public class Program
    {
        // NOTE TO INTERVIEWERS
        // I've added several "NOTE"s throughout the project explaining design choices
        // and drawbacks of the solution I've implemented in further detail.
        public static void Main(string[] args)
        {
            AutoMower.Run(args.Length > 0 ? args[0] : "./inputFile.txt");
        }
    }
}
