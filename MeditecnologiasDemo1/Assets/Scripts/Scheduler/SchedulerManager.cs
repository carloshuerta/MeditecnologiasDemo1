using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.Scripts.Models;
using MixedRealityToolkit.UX.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SchedulerManager : MonoBehaviour
{
    private const string OTHER_DATA_FORMAT = @"ID HI: {0}\nFecha Nacimiento: {1}\n{2}: {3}";

    private int PageIndex = 0;

    public int PageSize = 6;
    public Text CurrentDateTextBox;
    public GameObject PatientDataRowPrefab;
    public ObjectCollection ObjectParent;

    // Use this for initialization
    void Start()
    {
        this.CurrentDateTextBox.text = DateTime.Now.Date.ToString("dd/MM/yyyy");

        var turnPatients = GetPatients(this.PageIndex, this.PageSize);

        foreach (var turnPatient in turnPatients.OrderBy(x => x.TurnDate))
        {
            Initialize(turnPatient.TurnDate,
                turnPatient.Patient.Name,
                turnPatient.Patient.InternalID,
                turnPatient.Patient.BirthDate,
                turnPatient.Patient.DocumentType,
                turnPatient.Patient.DocumentNumber,
                turnPatient.Observation);
        }

        this.ObjectParent.UpdateCollection();
    }

    public IList<PatientTurn> GetPatients(int pageIndex, int rowCount)
    {
        //TODO: Replace to call the endpoint and get the schedule for the medic for the current date, paged.
        return new List<PatientTurn>
        {
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(10),
                Observation = "Instrumentos listos.\nEquipo listo.\nAnestesia lista.\nEsperando paciente.",
                Patient = new Patient
                {
                    Name = "Sebastian Gambolati",
                    BirthDate = DateTime.ParseExact("20/04/1980", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DocumentType = "DNI",
                    DocumentNumber = 28080291,
                    InternalID = 635063
                }
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(11),
                Observation = "Esperando quirófano.",
                Patient = new Patient
                {
                    Name = "Gustavo Bugna",
                    BirthDate = DateTime.ParseExact("02/07/1980", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DocumentType = "DNI",
                    DocumentNumber = 28563447,
                    InternalID = 78157
                }
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(12),
                Observation = "Esperando quirófano.",
                Patient = new Patient
                {
                    Name = "Carlos Huerta",
                    BirthDate = DateTime.ParseExact("18/11/1981", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DocumentType = "DNI",
                    DocumentNumber = 27153263,
                    InternalID = 125198
                }
            },

            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(13),
                Observation = "Esperando quirófano.",
                Patient = new Patient
                {
                    Name = "Saira Ruiz",
                    BirthDate = DateTime.ParseExact("01/02/1978", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DocumentType = "DNI",
                    DocumentNumber = 26477626,
                    InternalID = 6779310
                }
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(8),
                Observation = "Cirugia finalizada.",
                Patient = new Patient
                {
                    Name = "Lidia Bermudez",
                    BirthDate = DateTime.ParseExact("13/11/1956", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DocumentType = "DNI",
                    DocumentNumber = 3645893,
                    InternalID = 232491
                }
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(9),
                Observation = "Cirugia finalizada.",
                Patient = new Patient
                {
                    Name = "Hugo Alvornoz",
                    BirthDate = DateTime.ParseExact("12/07/1966", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DocumentType = "DNI",
                    DocumentNumber = 13431849,
                    InternalID = 984038
                }
            },
        };
    }

    public void Initialize(DateTime turnHour, 
        string name, 
        long internalID, 
        DateTime birthDate, 
        string documentType, 
        long documentNumber,
        string otherData)
    {
        var newRow = Instantiate(this.PatientDataRowPrefab);

        var txtHour = newRow.transform.Find("txtHour").gameObject;
        var txtName = newRow.transform.Find("txtName").gameObject;
        var txtPatientData = newRow.transform.Find("txtPatientData").gameObject;
        var txtOtherData = newRow.transform.Find("txtOtherData").gameObject;
        // TODO: Use to set the corresponding patient photo.
        var imgPhoto = newRow.transform.Find("Photo").gameObject;

        txtHour.GetComponentInChildren<Text>().text = turnHour.ToString("HH:mm");
        txtName.GetComponentInChildren<Text>().text = name;
        txtPatientData.GetComponentInChildren<Text>().text = string.Format(OTHER_DATA_FORMAT, internalID, birthDate, documentType, documentNumber);
        txtOtherData.GetComponentInChildren<Text>().text = otherData;

        newRow.transform.SetParent(this.ObjectParent.transform);
    }
}
