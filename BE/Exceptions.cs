using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Exceptions
    {
        public static Exception Invalid_Id_Exception = new Exception("Invalid ID!");
        public static Exception Id_Not_Found_Exception = new Exception("ID not found in system!");
        public static Exception No_Change_Exception = new Exception("No changes made. what is this tab is for again?");
        public static Exception Id_Already_Registered_Exception = new Exception("ID already registered in system, remember?");
        public static Exception File_Error_Exception = new Exception("File error has occurred");

        public static Exception No_Person_Selected_Exception = new Exception("Select Someone first");
        public static Exception First_Or_Last_Name_Is_Missing_Exception = new Exception("First or last name is missing, not that i'm judging");
        public static Exception Address_Is_Missing_Exception = new Exception("Address is missing");

        public static Exception No_Item_Selected_Exception = new Exception("Select Something first");
        public static Exception Name_Is_Missing_Exception = new Exception("Name is missing");

        public static Exception Contract_Person_Not_Selected_Exceptions = new Exception("Both an employee and an employer need to be selected, duh");

    }
}
