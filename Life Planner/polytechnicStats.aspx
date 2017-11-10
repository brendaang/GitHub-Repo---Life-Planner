<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="polytechnicStats.aspx.cs" Inherits="Life_Planner.polytechnicStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(document).ready(function () {
                $(".dropdown-menu li a").on("click", function () {
                    var iddiv = $(this).attr("id");
                   
                    $("#sp").hide();
                    $("#tp").hide();
                    $("#rp").hide();
                    $("#nyp").hide();
                    $("." + iddiv).show();
                });
            });
        });
    </script>
    <div class="horinzontal">
<h2>Polytechnic Statistic</h2>


     <div class="dropdown">
         
            <button type="button" class="btn btn dropdown-toggle" data-toggle="dropdown">
             Select
            <span class="caret"></span></button>
            <ul class="dropdown-menu">
                <li><a href="#"  id="container1">Singapore Polytechnic</a></li>
                <li><a href="#" id="container2">Temasek Polytechnic</a></li>
                <li><a href="#"  id="container3">Nanyang Polytechnic</a></li>
                <li><a href="#" id="container4">Republic Polytechnic</a></li>
            </ul>
        </div>
        </div>


    <div class="container">
       

        <div class="container1" id="sp" style="display: none">
            <h2>Singapore Polytechnic 
            </h2>

            <div class="col-md-6">
                <h4>Singapore Polytechnic Full-time Diploma Courses</h4>
                <div class="well well-sm">
                    <iframe width="520" height="600" src="https://data.gov.sg/dataset/singapore-polytechnic-full-time-diploma-courses/resource/57b4ca93-3a50-4623-8aba-59c050ca8db9/view/226bd6b4-08b6-429b-9e61-09060304477b" frameborder="0"></iframe>
<p>A list of the full-time diploma courses offered by Singapore Polytechnic</p>
                </div>
            </div>
            <div class="col-md-6">

                <h4>Singapore Polytechnic Full-time Diploma Course Fees</h4>
                <div class="well well-sm">
                    <iframe width="520" height="400" src="https://data.gov.sg/dataset/singapore-polytechnic-full-time-diploma-course-fees-annual/resource/efe5211f-312d-418b-91bd-4724333a406f/view/bbb2e928-66ad-45c2-92c2-9ab503cc061a" frameborder="0"></iframe>
                <p>To inform students and parents about Singapore Polytechnic's course fees and financial matters</p>
                </div>
            </div>
        </div>

        <div class="container2" id="tp" style="display: none">
            <h2>Temasek Polytechnic    
            </h2>
            <div class="col-md-6">
                <h4>Temasek Polytechnic Full-time Diploma Courses</h4>
                <div class="well well-sm">
                    <iframe width="520" height="600" src="https://data.gov.sg/dataset/temasek-polytechnic-full-time-courses-annual/resource/57a85ec7-1a80-4907-9b4e-5b8b9d047184/view/4b8d7a95-40b8-41be-ad43-9b3a46e33351" frameborder="0"></iframe>
               <p>A list of the full-time diploma courses offered by Temasek Polytechnic</p>
                    </div>
            </div>
            <div class="col-md-6">
                <h4>Temasek Polytechnic Planned Intake</h4>
                <div class="well well-sm">
                    <iframe width="520" height="400" src="https://data.gov.sg/dataset/temasek-polytechnic-full-time-enrolment-figures-breakdown-annual/resource/0dc0c8e3-2742-49c7-8271-81c84195a3a9/view/93a7d0aa-8d81-4242-9bed-d8d83d6e5e0c" frameborder="0"></iframe>
                  <p>This dataset contains the enrolment figures of Full-Time students, breakdown by school, course name, and gender.</p>
                    </div>
            </div>

        </div>

        <div class="container3" id="nyp" style="display: none">
            <h2>Nanyang Polytechnic    
            </h2>
            <div class="col-md-6">
                <h4>Nanyang Polytechnic Full-time Diploma Courses</h4>
                <div class="well well-sm">
                    <iframe width="520" height="600" src="https://data.gov.sg/dataset/nanyang-polytechnic-full-time-diploma-courses/resource/df488f8c-c9bf-4ee9-a724-6ff1df6b85df/view/28af4786-9aaa-465b-933f-9a98e8eba51c" frameborder="0"></iframe>
              <p>This dataset contains the list of full-time diploma courses that is offered by Nanyang Polytechnic. It includes the academic year that the course is being offered, course title and course code.

</p>
                    </div>
            </div>
            <div class="col-md-6">
                <h4>Nanyang Polytechnic Planned Intake</h4>
                <div class="well well-sm">
                    <iframe width="520" height="400" src="https://data.gov.sg/dataset/nanyang-polytechnic-planned-intake-annual/resource/ab38b0a5-c241-4f01-baf9-c52e9078e3b8/view/e8db828a-b428-4107-9b76-af3301fed566" frameborder="0"></iframe>
                <p>This dataset contains information on the Planned Student Intake for Nanyang Polytechnic.

JAE: Joint Admission Exercise</p>
                </div>
            </div>

        </div>


        <div class="container4" id="rp" style="display: none">

            <h2>Republic Polytechnic    
            </h2>
            <div class="col-md-11">
                <h4>Republic Polytechnic       Full-time Diploma Courses
                </h4>
                <div class="well well-sm">
                    <iframe width="1000" height="600" src="https://data.gov.sg/dataset/republic-polytechnic-full-time-diploma-courses/resource/c8d6b22a-4660-4576-a7cc-4bf20a9954bf/view/7c6ee10c-5555-482a-bb69-25fd77e9e168" frameborder="0"></iframe>
                <p>List of Full Time Diploma courses

</p>
                </div>
            </div>
        


        </div>
    </div>
</asp:Content>
