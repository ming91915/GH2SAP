﻿'I don't know it it's necessary to add both imports
Imports System.Collections.Generic
Imports System

Imports Grasshopper.Kernel
Imports Rhino.Geometry
Imports SAP2000v17



Public Class GH2SAPIO
    Inherits GH_Component
    ''' <summary>
    ''' Each implementation of GH_Component must provide a public 
    ''' constructor without any arguments.
    ''' Category represents the Tab in which the component will appear, 
    ''' Subcategory the panel. If you use non-existing tab or panel names, 
    ''' new tabs/panels will automatically be created.
    ''' </summary>
    Public Sub New()

        'Adding component information
        MyBase.New("GH2SAP_IO", "IO", _
                "Starts or links a SAP2000 instance to GH", _
                "GH2SAP", "System")
    End Sub

    ''' <summary>
    ''' Registers all the input parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterInputParams(pManager As GH_Component.GH_InputParamManager)


        Dim dir As String
        dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        dir = System.IO.Path.Combine(dir, "GH2SAP\DocumentName.sdb")

        pManager.AddBooleanParameter("IO", "IO", "If True, starts SAP2000 and/or links with it", GH_ParamAccess.item, False)
        pManager.AddIntegerParameter("Mode", "M", "Selects the link mode: 0 -> Static, 1 -> Dynamic...", GH_ParamAccess.item, 0)
        pManager.AddBooleanParameter("Link", "L", "If true the GH model links to an existing SAP2000 session, otherwise it starts a new or existing file", GH_ParamAccess.item, True)
        pManager.AddTextParameter("Path", "P", "Establish the filepath to an existing SAP2000 file or to a new one", GH_ParamAccess.item, dir)

    End Sub

    ''' <summary>
    ''' Registers all the output parameters for this component.
    ''' </summary>
    Protected Overrides Sub RegisterOutputParams(pManager As GH_Component.GH_OutputParamManager)

        pManager.AddTextParameter("Message", "M", "Output message", GH_ParamAccess.item)
        pManager.AddBooleanParameter("Flag", "F", "Flag that indicates when the component has finished", GH_ParamAccess.item)

    End Sub

    ''' <summary>
    ''' This is the method that actually does the work.
    ''' </summary>
    ''' <param name="DA">The DA object can be used to retrieve data from input parameters and 
    ''' to store data in output parameters.</param>
    Protected Overrides Sub SolveInstance(DA As IGH_DataAccess)

        'Declaring general variables
        Dim mySapObject As cOAPI = Nothing  'Creating the SapObject
        Dim bIO, bLink, bFlag As Boolean
        Dim intMode As Integer
        Dim strPath, strMessage As String

        'Passing values from component inputs to variables
        If (Not DA.GetData(0, bIO)) Then Return
        If (Not DA.GetData(1, intMode)) Then Return
        If (Not DA.GetData(2, bLink)) Then Return
        If (Not DA.GetData(3, strPath)) Then Return

        If bLink Then

            Try
                mySapObject = DirectCast(System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.SAP2000.API.SapObject"), cOAPI)

            Catch ex As Exception

                strMessage = "No running instance of the program found or failed to link"
            End Try

        Else

            strMessage = "File mode not implemented yet"

        End If


    End Sub

    ''' <summary>
    ''' Provides an Icon for every component that will be visible in the User Interface.
    ''' Icons need to be 24x24 pixels.
    ''' </summary>
    Protected Overrides ReadOnly Property Icon() As System.Drawing.Bitmap
        Get

            'Adding the icon to the component
            Return My.Resources.CSi_IO

        End Get
    End Property

    ''' <summary>
    ''' Each component must have a unique Guid to identify it. 
    ''' It is vital this Guid doesn't change otherwise old ghx files 
    ''' that use the old ID will partially fail during loading.
    ''' </summary>
    Public Overrides ReadOnly Property ComponentGuid() As Guid
        Get
            Return New Guid("{82f2260c-da98-49be-8585-233338f54372}")
        End Get
    End Property
End Class