using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.DataObjects;

namespace Fitness.BusinessLogic
{
    public static class BMRProvider
    {
        //Source: https://www.thecalculatorsite.com/articles/health/bmr-formula.php
        public static double CalculateBMR(double weightInKg, double heightInCm, int ageInYears, GenderType genderType)
        {
            switch (genderType)
            {
                case GenderType.Male:
                    return 66.47 + (13.75 * weightInKg) + (5.003 * heightInCm) - (6.755 * ageInYears);

                case GenderType.Female:
                    return 655.1 + (9.563 * weightInKg) + (1.85 * heightInCm) - (4.676 * ageInYears);                
            }

            return 0;
        }

    }
}
