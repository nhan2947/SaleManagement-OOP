
namespace R2S.Training.Main
{
    public static class Input
    {
        public static int GetInt()
        {
            bool valid = false;
            int value = 0;
            do
            {
                try
                {
                    value = Convert.ToInt32(Console.ReadLine());
                    valid = true;
                }
                catch (Exception ex)
                {
                    valid = false;
                    Console.WriteLine("Input is not valid: " + ex.Message);
                }
            }while(!valid);
            return value;
        }

        public static double GetDouble()
        {
            bool valid = false;
            double value = 0;
            do
            {
                try
                {
                    value = Convert.ToDouble(Console.ReadLine());
                    valid = true;
                }
                catch (Exception ex)
                {
                    valid = false;
                    Console.WriteLine("Input is not valid: " + ex.Message);
                }
            } while (!valid);
            return value;
        }
    }
}
