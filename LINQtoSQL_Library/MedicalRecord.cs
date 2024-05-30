﻿using System;

namespace LINQtoData_Library
{
    public class MedicalRecord
    {
        public int RecordID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public int BirthYear { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string BloodPressure { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ExaminationDate { get; set; }
    }

}
