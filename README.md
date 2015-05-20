# WAProject
WA Project, using Asp.MVC and Akka.Net on Mono

First attempt to use Akka.Net on Mono (with Asp.Net MVC)

##Project Specification

Make a simple web interface where users can upload a CSV file consisting of a column called Id 

(consisting of unique int identifiers), another column called Decision consisting of either a 0 or 1 

and an arbitrary number of "variable" columns consisting of numeric data.

The app should parse the csv file and remove any records (rows) which meet both of the 

following conditions:

● Have a Decision of 0

● For each variable (column), no value falls between FMIN and FMAX.

Where FMIN and FMAX are the smallest and largest value for that variable across all records 

with a decision value of 1.

This new stripped down data should then be shown to the user in tabular format.
