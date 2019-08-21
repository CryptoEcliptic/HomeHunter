//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace HomeHunter.Models.MLModels
{
    public class ModelInput
    {
        [ColumnName("Id"), LoadColumn(0)]
        public float Id { get; set; }


        [ColumnName("Size"), LoadColumn(1)]
        public float Size { get; set; }


        [ColumnName("Floor"), LoadColumn(2)]
        public float Floor { get; set; }


        [ColumnName("TotalFloors"), LoadColumn(3)]
        public float TotalFloors { get; set; }


        [ColumnName("District"), LoadColumn(4)]
        public string District { get; set; }


        [ColumnName("Year"), LoadColumn(5)]
        public float Year { get; set; }


        [ColumnName("Type"), LoadColumn(6)]
        public string Type { get; set; }


        [ColumnName("BuildingType"), LoadColumn(7)]
        public string BuildingType { get; set; }


        [ColumnName("Price"), LoadColumn(8)]
        public float Price { get; set; }


    }
}
