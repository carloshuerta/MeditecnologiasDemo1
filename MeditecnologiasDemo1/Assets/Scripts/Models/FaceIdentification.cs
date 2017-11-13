using System;

[Serializable]
public class FaceAPIResult
{
    public FaceIdentification[] values;
}

[Serializable]
public class FaceIdentification
{
    public string id;

    public string name;

    public string confidence;

    public string userData;

    public long HCId;
}