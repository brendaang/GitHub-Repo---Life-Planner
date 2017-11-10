<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="juniorCollegeStats.aspx.cs" Inherits="Life_Planner.juniorCollegeStats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 


    
    <div class="container">
        <h2>JC Statistic
        </h2>

        <div class="col-md-6">
            <h4>JC Enrolment - Pre-University, By Level and Course  </h4>
            <div class="well well-sm">
                <iframe width="520" height="600" src="https://data.gov.sg/dataset/enrolment-pre-university-by-level/resource/8b436e71-ef7d-4a92-86e5-dfdebdc25d8c/view/16d236b8-688c-463d-a664-ce67a8d4b3e4" frameborder="0"></iframe>
                <p>Pre-U enrolment by level and course.
<br />Since 2006, as part of a new broad-based JC education, students are required to do at least one subject outside their area of specialization.<br /> For example, a Science course student is required to take at least one Humanities subject and an Arts course student is required to take at least one Science subject.
</p>
            </div>
        </div>
        <div class="col-md-6">

            <h4>Enrolment - Pre-University, By Age</h4>
            <div class="well well-sm">
                <iframe width="520" height="400" src="https://data.gov.sg/dataset/enrolment-preu-by-age/resource/c8f793e3-d141-4cbf-a307-a4ddbd79afe0/view/578b9c14-ac2e-45a9-b737-3dc9983bff90" frameborder="0"></iframe>
            <p>Pre-University enrolment by age.
<br />Includes pre-university students such as those in Year 5 and 6 of the Integrated Programme.
<br />Includes Government, Govt-aided, Independent and Specialised Independent schools.
<br />Age is as at the start of the year.</p>
            </div>
        </div>
    </div>
</asp:Content>
