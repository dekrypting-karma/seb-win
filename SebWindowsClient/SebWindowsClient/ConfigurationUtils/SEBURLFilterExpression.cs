﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SebWindowsClient.ConfigurationUtils
{
    class SEBURLFilterExpression
    {
        private string scheme;
        private string user;
        private string password;
        private string host;
        private int port;
        private string path;
        private string query;
        private string fragment;


        public SEBURLFilterExpression(string filterExpressionString)
        {
            // Check if filter expression contains a scheme
            string newScheme = "";
            Uri URLFromString = null;

            if (filterExpressionString != null && filterExpressionString.Length > 0)
            {
                int schemeDelimiter = filterExpressionString.IndexOf("://");

                if (schemeDelimiter != -1)
                {
                    // Filter expression contains a scheme: save it and replace it with http
                    // (in case scheme contains a wildcard)
                    newScheme = filterExpressionString.Substring(0, schemeDelimiter);
                    filterExpressionString = "http" + filterExpressionString.Substring(schemeDelimiter);
                    // Convert filter expression string to a NSURL
                    if (!Uri.TryCreate(filterExpressionString, UriKind.RelativeOrAbsolute, out URLFromString))
                    {
                        return;
                    }
                }
                else
                {
                    // Filter expression doesn't contain a scheme followed by an authority part,
                    // check for scheme followed by only a path (like about:blank or data:...)
                    // Convert filter expression string to a NSURL
                    if (!Uri.TryCreate(filterExpressionString, UriKind.RelativeOrAbsolute, out URLFromString))
                    {
                        return;
                    }
                    try
                    {
                        newScheme = URLFromString.Scheme;
                    }
                    catch (Exception)
                    {
                        // Probably a relative URI without scheme
                        // Temporary prefix it with a http:// scheme
                        filterExpressionString = "http" + filterExpressionString;
                        // Convert filter expression string to a NSURL
                        if (!Uri.TryCreate(filterExpressionString, UriKind.RelativeOrAbsolute, out URLFromString))
                        {
                            return;
                        }
                    }
                }

                /// Convert NSURL to a SEBURLFilterExpression
                // Use the saved scheme instead of the temporary http://

                this.scheme = newScheme;
                this.user = URLFromString.UserInfo;
                //this.password = URLFromString.;
                this.host = URLFromString.Host;
                this.port = URLFromString.Port;
                this.path = URLFromString.AbsolutePath.TrimEnd(new char[] { '/' });
                this.query = URLFromString.Query;
                this.fragment = URLFromString.Fragment;

            }

            //    // Create a muatable array to hold results of scanning the filter expression for relevant separators
            //    NSMutableArray *foundSeparators = [NSMutableArray new];
            //    
            //    /// Scan the filter expression string for all relevant URL separators
            //    // Scan for the scheme separator
            //    NSString *separator = @"://";
            //    NSRange scanResult = [filterExpressionString rangeOfString:separator];
            //    if (scanResult.location != NSNotFound) {
            //        NSDictionary *foundSeparator = @{
            //                                         @"separator" : separator,
            //                                         @"location" : [NSNumber numberWithInteger:scanResult.location],
            //                                         @"length" : [NSNumber numberWithInteger:scanResult.length],
            //                                         };
            //
            //        [foundSeparators addObject:foundSeparator];
            //    }


        }

        public SEBURLFilterExpression(string scheme, string user, string password, string host, int port, string path, string query, string fragment)
        {
            this.scheme = scheme;
            this.user = user;
            this.password = password;
            this.host = host;
            this.port = port;
            this.path = path;
            this.query = query;
            this.fragment = fragment;
        }

        /*

        - (NSString*) string
        {
            //    NSURL *newURL;
            NSMutableString* expressionString = [NSMutableString new];
            if (_scheme.length > 0) {
                if (_host.length > 0) {
                    [expressionString appendFormat:@"%@://", _scheme];
                } else {
                    [expressionString appendFormat:@"%@:", _scheme];
                }
            }
            if (_user.length > 0) {
                [expressionString appendString:_user];

                if (_password.length > 0) {
                    [expressionString appendFormat:@":%@@", _password];
                } else {
                    [expressionString appendString:@"@"];
                }
            }
            if (_host.length > 0) {
                [expressionString appendString:_host];
            }
            if (_port && (_port.integerValue > 0) && (_port.integerValue <= 65535)) {
                [expressionString appendFormat:@":%@", _port.stringValue];
            }
            if (_path.length > 0) {
                if ([_path hasPrefix:@"/"]) {
                    [expressionString appendString:_path];
                } else {
                    [expressionString appendFormat:@"/%@", _path];
                }

        //        if (![_path hasSuffix:@"/"]) {
        //            [expressionString appendString:@"/"];
        //        }
            }
            if (_query.length > 0) {
                [expressionString appendFormat:@"?%@", _query];
            }
            if (_fragment.length > 0) {
                [expressionString appendFormat:@"#%@", _fragment];
            }

            return expressionString;
        }
            }
            */
    }
}
