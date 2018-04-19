using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Scheduler;
using MixedRealityToolkit.UX.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SchedulerManager : MonoBehaviour
{
    private const string OTHER_DATA_FORMAT = @"ID HI: {0}\nFecha Nacimiento: {1}\n{2}: {3}";

    private int PageIndex = 0;

    public int PageSize = 6;
    public GameObject PatientDataRowPrefab;
    public ObjectCollection ObjectParent;

    // Use this for initialization
    void Start()
    {
        var turnPatients = GetPatients(this.PageIndex, this.PageSize);

        foreach (var turnPatient in turnPatients.OrderBy(x => x.TurnDate))
        {
            Initialize(turnPatient.TurnDate,
                turnPatient.Patient.PatientId,
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
                Patient = PatientsRepository.GetPatient(1)
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(11),
                Observation = "Esperando quirófano.",
                Patient = PatientsRepository.GetPatient(2)
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(12),
                Observation = "Esperando quirófano.",
                Patient = PatientsRepository.GetPatient(3)
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(13),
                Observation = "Esperando quirófano.",
                Patient = PatientsRepository.GetPatient(4)
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(8),
                Observation = "Cirugia finalizada.",
                Patient = PatientsRepository.GetPatient(5)
            },
            new PatientTurn
            {
                TurnDate = DateTime.Now.Date.AddHours(9),
                Observation = "Cirugia finalizada.",
                Patient = PatientsRepository.GetPatient(6)
            },
        };
    }

    public void Initialize(DateTime turnHour, 
        int patientId,
        string patientName, 
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
        var btnViewStudies = newRow.transform.Find("hbtnViewStudies").gameObject;
        var btnViewPatientData = newRow.transform.Find("hbtnViewPatientData").gameObject;

        // TODO: Use to set the corresponding patient photo.
        var imgPhoto = newRow.transform.Find("Photo").gameObject;

        txtHour.GetComponentInChildren<Text>().text = turnHour.ToString("HH:mm");
        txtName.GetComponentInChildren<Text>().text = patientName;
        txtPatientData.GetComponentInChildren<Text>().text = string.Format(OTHER_DATA_FORMAT, internalID, birthDate, documentType, documentNumber);
        txtOtherData.GetComponentInChildren<Text>().text = otherData;

        btnViewStudies.GetComponent<ViewStudies>().PatientID = patientId;
        btnViewPatientData.GetComponent<ViewPatientData>().PatientID = patientId;

        newRow.transform.SetParent(this.ObjectParent.transform);
    }
}
