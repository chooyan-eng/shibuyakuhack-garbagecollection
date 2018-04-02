module.exports = function (context,input) {
    //context.log('Document Id:',input[0].id);
    //context.log('all document:,input);

    input.forEach(function(tweet){
        context.log(tweet.media_url[0]);
    }); 

    context.done();
};
