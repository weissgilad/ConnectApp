using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    public class Specialization : ICloneable
    {


        public int Id { get; set; }
        public SpecType Type { get; set; }
        public string SpecialtyName { get; set; }

        public double MinimumSalary { get; set; }
        public double MaximumSalary { get; set; }



        public override string ToString()
        {
            return Id + "\n   " + SpecialtyName;
        }

        public object Clone()
        {
            return MemberwiseClone();
            //Specialization old = new Specialization();
            //old.Id            = Id;
            //old.Type          = Type;
            //old.SpecialtyName = SpecialtyName;
            //old.MinimumSalary = MinimumSalary;
            //old.MaximumSalary = MaximumSalary;
            //return old;
        }

        public override bool Equals(object obj)
        {
            if (obj is Specialization)
            {
                Specialization check = (Specialization)obj;
                return Id == check.Id &&
                        Type == check.Type &&
                        SpecialtyName == check.SpecialtyName &&
                        MinimumSalary == check.MinimumSalary &&
                        MaximumSalary == check.MaximumSalary;
            }
            else return false;
        }
    }
}
