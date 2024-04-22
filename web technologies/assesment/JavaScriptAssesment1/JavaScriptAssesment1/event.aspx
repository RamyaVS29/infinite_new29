<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Multidiv.aspx.cs" Inherits="JavaScriptAssesment1.Multiplication" %>

<!DOCTYPE html>
<html>
<head>
    <title>Button Click Event</title>
</head>
<body>
    <h2 style="color:deeppink;">Click the Button</h2>
    <button id="MyyButton">Click Me!</button>

    <script>
     
        function handleClick() {
            console.log("Button clicked!");
        }

      
        var button = document.getElementById('MyyButton');

    
        button.addEventListener('click', handleClick);
    </script>
</body>
</html>
