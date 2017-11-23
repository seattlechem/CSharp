

$("button").click(function(){
    $.get("/random", function(response){
        $("p").remove();
        $("#counter").append("<p>"+ "passcode #"+ response.counter +"</p>")
        $("#content").append("<p>"+ response.output +"</p>");
        console.log(response);
    })
})

