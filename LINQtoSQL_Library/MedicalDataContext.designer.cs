﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LINQtoSQL_Library
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="OOPaP_67")]
	public partial class MedicalDataContextDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Определения метода расширяемости
    partial void OnCreated();
    partial void InsertDoctors(Doctors instance);
    partial void UpdateDoctors(Doctors instance);
    partial void DeleteDoctors(Doctors instance);
    partial void InsertMedicalRecords(MedicalRecords instance);
    partial void UpdateMedicalRecords(MedicalRecords instance);
    partial void DeleteMedicalRecords(MedicalRecords instance);
    partial void InsertPatients(Patients instance);
    partial void UpdatePatients(Patients instance);
    partial void DeletePatients(Patients instance);
    #endregion
		
		public MedicalDataContextDataContext() : 
				base(global::LINQtoSQL_Library.Properties.Settings.Default.OOPaP_67ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MedicalDataContextDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MedicalDataContextDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MedicalDataContextDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MedicalDataContextDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Doctors> Doctors
		{
			get
			{
				return this.GetTable<Doctors>();
			}
		}
		
		public System.Data.Linq.Table<MedicalRecords> MedicalRecords
		{
			get
			{
				return this.GetTable<MedicalRecords>();
			}
		}
		
		public System.Data.Linq.Table<Patients> Patients
		{
			get
			{
				return this.GetTable<Patients>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Doctors")]
	public partial class Doctors : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _DoctorID;
		
		private string _FullName;
		
		private EntitySet<MedicalRecords> _MedicalRecords;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDoctorIDChanging(int value);
    partial void OnDoctorIDChanged();
    partial void OnFullNameChanging(string value);
    partial void OnFullNameChanged();
    #endregion
		
		public Doctors()
		{
			this._MedicalRecords = new EntitySet<MedicalRecords>(new Action<MedicalRecords>(this.attach_MedicalRecords), new Action<MedicalRecords>(this.detach_MedicalRecords));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DoctorID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int DoctorID
		{
			get
			{
				return this._DoctorID;
			}
			set
			{
				if ((this._DoctorID != value))
				{
					this.OnDoctorIDChanging(value);
					this.SendPropertyChanging();
					this._DoctorID = value;
					this.SendPropertyChanged("DoctorID");
					this.OnDoctorIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FullName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				if ((this._FullName != value))
				{
					this.OnFullNameChanging(value);
					this.SendPropertyChanging();
					this._FullName = value;
					this.SendPropertyChanged("FullName");
					this.OnFullNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Doctors_MedicalRecords", Storage="_MedicalRecords", ThisKey="DoctorID", OtherKey="DoctorID")]
		public EntitySet<MedicalRecords> MedicalRecords
		{
			get
			{
				return this._MedicalRecords;
			}
			set
			{
				this._MedicalRecords.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_MedicalRecords(MedicalRecords entity)
		{
			this.SendPropertyChanging();
			entity.Doctors = this;
		}
		
		private void detach_MedicalRecords(MedicalRecords entity)
		{
			this.SendPropertyChanging();
			entity.Doctors = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MedicalRecords")]
	public partial class MedicalRecords : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RecordID;
		
		private int _DoctorID;
		
		private int _PatientID;
		
		private string _Diagnosis;
		
		private System.DateTime _ExaminationDate;
		
		private EntityRef<Doctors> _Doctors;
		
		private EntityRef<Patients> _Patients;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRecordIDChanging(int value);
    partial void OnRecordIDChanged();
    partial void OnDoctorIDChanging(int value);
    partial void OnDoctorIDChanged();
    partial void OnPatientIDChanging(int value);
    partial void OnPatientIDChanged();
    partial void OnDiagnosisChanging(string value);
    partial void OnDiagnosisChanged();
    partial void OnExaminationDateChanging(System.DateTime value);
    partial void OnExaminationDateChanged();
    #endregion
		
		public MedicalRecords()
		{
			this._Doctors = default(EntityRef<Doctors>);
			this._Patients = default(EntityRef<Patients>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RecordID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int RecordID
		{
			get
			{
				return this._RecordID;
			}
			set
			{
				if ((this._RecordID != value))
				{
					this.OnRecordIDChanging(value);
					this.SendPropertyChanging();
					this._RecordID = value;
					this.SendPropertyChanged("RecordID");
					this.OnRecordIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DoctorID", DbType="Int NOT NULL")]
		public int DoctorID
		{
			get
			{
				return this._DoctorID;
			}
			set
			{
				if ((this._DoctorID != value))
				{
					if (this._Doctors.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDoctorIDChanging(value);
					this.SendPropertyChanging();
					this._DoctorID = value;
					this.SendPropertyChanged("DoctorID");
					this.OnDoctorIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PatientID", DbType="Int NOT NULL")]
		public int PatientID
		{
			get
			{
				return this._PatientID;
			}
			set
			{
				if ((this._PatientID != value))
				{
					if (this._Patients.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPatientIDChanging(value);
					this.SendPropertyChanging();
					this._PatientID = value;
					this.SendPropertyChanged("PatientID");
					this.OnPatientIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Diagnosis", DbType="NVarChar(255) NOT NULL", CanBeNull=false)]
		public string Diagnosis
		{
			get
			{
				return this._Diagnosis;
			}
			set
			{
				if ((this._Diagnosis != value))
				{
					this.OnDiagnosisChanging(value);
					this.SendPropertyChanging();
					this._Diagnosis = value;
					this.SendPropertyChanged("Diagnosis");
					this.OnDiagnosisChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ExaminationDate", DbType="Date NOT NULL")]
		public System.DateTime ExaminationDate
		{
			get
			{
				return this._ExaminationDate;
			}
			set
			{
				if ((this._ExaminationDate != value))
				{
					this.OnExaminationDateChanging(value);
					this.SendPropertyChanging();
					this._ExaminationDate = value;
					this.SendPropertyChanged("ExaminationDate");
					this.OnExaminationDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Doctors_MedicalRecords", Storage="_Doctors", ThisKey="DoctorID", OtherKey="DoctorID", IsForeignKey=true)]
		public Doctors Doctors
		{
			get
			{
				return this._Doctors.Entity;
			}
			set
			{
				Doctors previousValue = this._Doctors.Entity;
				if (((previousValue != value) 
							|| (this._Doctors.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Doctors.Entity = null;
						previousValue.MedicalRecords.Remove(this);
					}
					this._Doctors.Entity = value;
					if ((value != null))
					{
						value.MedicalRecords.Add(this);
						this._DoctorID = value.DoctorID;
					}
					else
					{
						this._DoctorID = default(int);
					}
					this.SendPropertyChanged("Doctors");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Patients_MedicalRecords", Storage="_Patients", ThisKey="PatientID", OtherKey="PatientID", IsForeignKey=true)]
		public Patients Patients
		{
			get
			{
				return this._Patients.Entity;
			}
			set
			{
				Patients previousValue = this._Patients.Entity;
				if (((previousValue != value) 
							|| (this._Patients.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Patients.Entity = null;
						previousValue.MedicalRecords.Remove(this);
					}
					this._Patients.Entity = value;
					if ((value != null))
					{
						value.MedicalRecords.Add(this);
						this._PatientID = value.PatientID;
					}
					else
					{
						this._PatientID = default(int);
					}
					this.SendPropertyChanged("Patients");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Patients")]
	public partial class Patients : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PatientID;
		
		private string _FullName;
		
		private int _BirthYear;
		
		private decimal _Height;
		
		private decimal _Weight;
		
		private string _BloodPressure;
		
		private EntitySet<MedicalRecords> _MedicalRecords;
		
    #region Определения метода расширяемости
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPatientIDChanging(int value);
    partial void OnPatientIDChanged();
    partial void OnFullNameChanging(string value);
    partial void OnFullNameChanged();
    partial void OnBirthYearChanging(int value);
    partial void OnBirthYearChanged();
    partial void OnHeightChanging(decimal value);
    partial void OnHeightChanged();
    partial void OnWeightChanging(decimal value);
    partial void OnWeightChanged();
    partial void OnBloodPressureChanging(string value);
    partial void OnBloodPressureChanged();
    #endregion
		
		public Patients()
		{
			this._MedicalRecords = new EntitySet<MedicalRecords>(new Action<MedicalRecords>(this.attach_MedicalRecords), new Action<MedicalRecords>(this.detach_MedicalRecords));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PatientID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int PatientID
		{
			get
			{
				return this._PatientID;
			}
			set
			{
				if ((this._PatientID != value))
				{
					this.OnPatientIDChanging(value);
					this.SendPropertyChanging();
					this._PatientID = value;
					this.SendPropertyChanged("PatientID");
					this.OnPatientIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FullName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				if ((this._FullName != value))
				{
					this.OnFullNameChanging(value);
					this.SendPropertyChanging();
					this._FullName = value;
					this.SendPropertyChanged("FullName");
					this.OnFullNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BirthYear", DbType="Int NOT NULL")]
		public int BirthYear
		{
			get
			{
				return this._BirthYear;
			}
			set
			{
				if ((this._BirthYear != value))
				{
					this.OnBirthYearChanging(value);
					this.SendPropertyChanging();
					this._BirthYear = value;
					this.SendPropertyChanged("BirthYear");
					this.OnBirthYearChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Height", DbType="Decimal(5,2) NOT NULL")]
		public decimal Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				if ((this._Height != value))
				{
					this.OnHeightChanging(value);
					this.SendPropertyChanging();
					this._Height = value;
					this.SendPropertyChanged("Height");
					this.OnHeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Weight", DbType="Decimal(5,2) NOT NULL")]
		public decimal Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if ((this._Weight != value))
				{
					this.OnWeightChanging(value);
					this.SendPropertyChanging();
					this._Weight = value;
					this.SendPropertyChanged("Weight");
					this.OnWeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BloodPressure", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string BloodPressure
		{
			get
			{
				return this._BloodPressure;
			}
			set
			{
				if ((this._BloodPressure != value))
				{
					this.OnBloodPressureChanging(value);
					this.SendPropertyChanging();
					this._BloodPressure = value;
					this.SendPropertyChanged("BloodPressure");
					this.OnBloodPressureChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Patients_MedicalRecords", Storage="_MedicalRecords", ThisKey="PatientID", OtherKey="PatientID")]
		public EntitySet<MedicalRecords> MedicalRecords
		{
			get
			{
				return this._MedicalRecords;
			}
			set
			{
				this._MedicalRecords.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_MedicalRecords(MedicalRecords entity)
		{
			this.SendPropertyChanging();
			entity.Patients = this;
		}
		
		private void detach_MedicalRecords(MedicalRecords entity)
		{
			this.SendPropertyChanging();
			entity.Patients = null;
		}
	}
}
#pragma warning restore 1591
