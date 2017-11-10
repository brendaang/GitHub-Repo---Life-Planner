<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="iteStats.aspx.cs" Inherits="Life_Planner.iteStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <div class="container">
        <h2>ITE Statistic
        </h2>

        <div class="col-md-6">
            <h4>ITE Course Catalog

            </h4>
            <div class="well well-sm">
                <iframe width="520" height="600" src="https://data.gov.sg/dataset/ite-course-catalog/resource/248656ef-f157-479a-aaef-a4ebd3bd6205/view/fe002a4c-c840-49e6-a6e4-ac44016056cf" frameborder="0"></iframe>

            </div>
        </div>
        <div class="col-md-6">

            <h4>Fees for ITE Full-Time Diploma Courses by Year</h4>
            <div class="well well-sm">
                <iframe width="520" height="400" src="https://data.gov.sg/dataset/fees-for-ite-full-time-diploma-courses/resource/496fb5c1-1692-4214-963e-df371d024917/view/639a9a25-dc54-475b-af71-ff50dd7346c8" frameborder="0"></iframe>
            </div>
        </div>
    </div>

</asp:Content>
