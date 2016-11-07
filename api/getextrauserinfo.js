
//module.exports = {
    //"get": function (req, res, next) {
    //}
//}

exports.get = function(request, response) {
    var currentUser = request.user;
    currentUser.getIdentities({
        success: function (identities) {
            var url = null;
            var oauth = null;
            if (identities.google) {
                url = 
         'https://www.googleapis.com/oauth2/v3/userinfo?access_token='
         + identities.google.accessToken;
            } else if (identities.facebook) {
                url = 'https://graph.facebook.com/me?access_token='
                + identities.facebook.accessToken;
            } else if (identities.twitter) {
                var userId = currentUser.userId;
                var twitterId = userId.substring(userId.indexOf(':') + 1);
                url = 'https://api.twitter.com/1.1/users/show.json?user_id='
                + twitterId;
                var consumerKey = process.env.MS_TwitterConsumerKey;
                var consumerSecret = process.env.MS_TwitterConsumerSecret;
                oauth = {
                    consumer_key: consumerKey,
                    consumer_secret: consumerSecret,
                    token: identities.twitter.accessToken,
                    token_secret: identities.twitter.accessTokenSecret
                };
            }
 
            if (url) {
                var requestCallback = function (err, resp, body) {
                    if (err || resp.statusCode !== 200) {
                        console.error('Error sending data to the provider: ', 
                        err);
                        request.respond(statusCodes.INTERNAL_SERVER_ERROR, 
                        body);
                    } else {
                        try {
                            var userData = JSON.parse(body);
                            response.send(statusCodes.OK, 
                { message : userData });
                        } catch (ex) {
                            console.error(
                 'Error parsing response from the provider API: ', 
                               ex);
                            request.respond(statusCodes.INTERNAL_SERVER_ERROR,
                               ex);
                        }
                    }
                }
                var req = require('request');
                var reqOptions = {
                    uri: url,
                    headers: { Accept: "application/json" }
                };
                if (oauth) {
                    reqOptions.oauth = oauth;
                }
                req(reqOptions, requestCallback);
            } else {
                response.send(statusCodes.OK, { message : 'Error!' });
            }
        }
    });
};