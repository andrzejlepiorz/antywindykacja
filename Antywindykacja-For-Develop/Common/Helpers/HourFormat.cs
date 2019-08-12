namespace Common.Helpers
{
    public static class HourFormat
    {
        public static bool CorrectHourFormat(int hourFrom, int hourTo)
        {
            if (hourFrom >= 0 && hourFrom <= 23)
            {
                if (hourTo >= 0 && hourTo <= 23)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
