<%@ Page Title="Life Planner" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Life_Planner.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="container">
    <div class="row">
        <div class="col-md-6">
      
            <div class="well well-sm">

                    <fieldset>

                        <legend class="text-xs-center header">Contact us</legend>
                        <div class="form-group">
                            <div class="col-md-10 offset-md-1">
                                <input id="fname" name="name" type="text" placeholder="First Name" class="form-control">
                            </div>
                        </div>
                          <br />
        <br />
        <br />
                        <div class="form-group">
                            <div class="col-md-10 offset-md-1">
                                <input id="lname" name="name" type="text" placeholder="Last Name" class="form-control">
                            </div>
                        </div>
                          <br />
        <br />
        <br />

                        <div class="form-group">
                            <div class="col-md-10 offset-md-1">
                                <input id="email" name="email" type="text" placeholder="Email Address" class="form-control">
                            </div>
                        </div>
                          <br />
        <br />
        <br />

                        <div class="form-group">
                            <div class="col-md-10 offset-md-1">
                                <input id="phone" name="phone" type="text" placeholder="Phone" class="form-control">
                            </div>
                        </div>
                          <br />
        <br />
        <br />

                        <div class="form-group">
                      
                            <div class="col-md-10 offset-md-1">
                                <textarea class="form-control" id="message" name="message" placeholder="Enter your massage for us here. We will get back to you within 2 business days." rows="7"></textarea>
                            </div>
                        </div>
                          <br />
        <br />
        <br />


                        <div class="form-group">
                     
            <div class="col-md-10">
                <asp:Button ID="Button1" runat="server" CssClass="form-control btn btn-primary btn-lg " Text="Submit" Width="200px"  />
            </div>
                            
                        </div>
                          <br />
        <br />
        <br />

                 
                    </fieldset>
            
                
            </div>
        </div>
        <div class="col-md-6">
            <div>
                <div class="card ">
                    <div class="text-xs-center header">Our Office</div>
                    <div class="card-block text-xs-center">
                          <h4>Mail</h4>
                        <div>
                        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
                        </div>
                        <h4>Address</h4>
                        <div>
                       50 Nanyang Ave, Singapore 639798<br />
            
                        </div>
                       
                        <hr />
                        <div id="map1" class="map">
                        </div>
                    </div>
                </div>
           
                </div>
            </div>
        </div>
       </div>




<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAUsj9gvOXO9uG-w9On5u45hK7pfxe3giI&callback=init_map1"></script>

<script type="text/javascript">
    jQuery(function ($) {
        function init_map1() {
            var myLocation = new google.maps.LatLng(1.34831, 103.683135);
            var mapOptions = {
                center: myLocation,
                zoom: 16
            };
            var marker = new google.maps.Marker({
                position: myLocation,
                title: "Property Location"
            });
            var map = new google.maps.Map(document.getElementById("map1"),
                mapOptions);
            marker.setMap(map);
        }
        init_map1();
    });
</script>

<style>
    .map {
        min-width: 300px;
        min-height: 300px;
        width: 100%;
        height: 100%;
    }

    .header {
        background-color: #F5F5F5;
        color: #36A0FF;
        height: 70px;
        font-size: 27px;
        padding: 10px;
    }
</style>

</asp:Content>
