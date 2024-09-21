# Razorblue Assessment
Assessment consists for following projects:

* Razorblue.Anagram
* Razorblue.DataImport
* Razorblue.IPFiltering
* Razorblue.FizzBuzz

## Notes

* All projects can be launched independently and provide interactive console for interactions.
* All implementations assume that source data is valid.
* Razorblue.IPFiltering uses in-memory data list for holding Single IPs, IP Range & CIDR which is fixed and should be referenced while testing.
* Technical Test Data.csv provided for Razorblue.DataImport contains inconsistent data which will result in multiple fuel type files like 'Petral', 'Desel', 'Deisel' etc.
* Technical Test Data.csv provided for Razorblue.DataImport contains one inconsistent Car Registration (which is a duplicate primary key with different spaces). This case is so not handled, as it requires further info on how to select from duplicates v/s invalid registrations. 
