<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validator.aspx.cs" Inherits="Validations1.Validator" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title></title>

</head>

<body>
      

    <form id="form1" runat="server">

        <div>

            <h2 style="color:darkmagenta;font-size:20px;text-align:left;"> Validator</h2>

            <p style="color:chocolate;">===============Insert Your Info=============</p>

            <div>

                <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" style="color: darkviolet;" >Name:</asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

                <br />

                <br />

            </div>

            <div>

                <asp:Label ID="lblFamilyName" runat="server" AssociatedControlID="txtFamilyName" style="color: darkviolet;">Family Name:</asp:Label>

                &nbsp;

                <asp:TextBox ID="txtFamilyName" runat="server"></asp:TextBox>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *differs from name<br />

                <br />

            </div>

            <div>

                <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtAddress" style="color: darkviolet;">Address:</asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *atlest 2 chars<br />

                <br />

            </div>

            <div>

                <asp:Label ID="lblCity" runat="server" AssociatedControlID="txtCity" style="color: darkviolet;">City:</asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *atleast 2 chars<br />

                <br />

            </div>

            <div>

                <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="txtZipCode" style="color: darkviolet;">Zip Code:</asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *(xxxxx)<br />

                <br />

            </div>

            <div>

                <asp:Label ID="lblPhone" runat="server" AssociatedControlID="txtPhone" style="color: darkviolet;">Phone:</asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *(xx-xxxxxxx or xxx-xxxxxxx)<br />

                <br />

            </div>

            <div>

                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" style="color: darkviolet;">Email:</asp:Label>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                <br />
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;

                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="mailto:*examaple@example.com">*examaple@example.com</a><br />
                <br />
                <br />
                <br />

            </div>

            <div>

                &nbsp;&nbsp;<asp:Button ID="btnCheck" runat="server" Text="Check" style="color: darkblue;" OnClick="btnCheck_Click" />

                <br />

            </div>

            <h4>&nbsp;</h4>

             <div id="details" runat="server">

            </div>

        </div>

    </form>

</body>

</html>

