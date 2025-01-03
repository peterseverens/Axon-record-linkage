# Axon record linkage

Scope

- match data from different sources in criminal investigations
- prepare research data from different sources

How it works

Axon is developed to link datasets that cannot be linked on hard or even soft criteria. Axon links datasets by evaluating and then applying Bayesian relations between fields in both sets.
Axon uses a twostep approach.
Step 1	The records are linked on hard criteria like city and month of birth. Each record form dataset 1 has then several candidate records in dataset 2.
Step 2	Axon derives Bayesian relations between the content in other fields like telephone numbers called, disease code in set 1 and care code in set 2 or income in set 1 and type of neighborhood in set 2. Bayesian relations are calculated between all fields in both sets.
Step 3	Axon uses the derived knowledge to match the datasets again. The matching now becomes better. The number of candidate found are now less than in step 1. Then Axon derives new estimations based on the new match and matches again. Now the match is even better and the number of candidates shrinks even more. Axon can proceed with this until there are no gains found. Mostly three to four loops are sufficient.   

Conditions

There has to be a relation between field in set 1 and set 2. If for example set 1 uses a different identity number for shops then in set 2 a relation exists. Or set 1 uses a disease coding system and set 2 a care coding system. The relation is only not yet known.
The system can handle billions of records, at least 256 fields in both sets and 32000 categories in each field. The only limit is the ram memory of your computer/server.
Axon uses a Bayesian or a Chi Square estimation. Therefore the estimation is based on categories, not on ratio’s. A field like income has to be converted to categories.

Prepare data set

Axon expects a dataset with simple headers to point to the number of independent, dependent and singular variables
In the example below there are 5 independent, 6 dependent and one singular variables (5/6/1) with their names, starting with the name 'InDep0'. The name should be followed with only the delimiter (assuming the missing value is an empty field) or a category (like 'InDep3' and 'InDep4').
The data set is arranged in groups. The first record is always the first record of the independent dataset. The records after that are the candidates (after a raw matching with the independent dataset). Both the independent and the dependent records start with the block number followed by the identifying key of the record. Then the field values follow.
Only the dependent records have singular fields values (after the dependent field values)

Choose the Test tab to build a test set. You can choose the number of field (variables) and the number of records. It is recommended to test whether the dataset you are planning to input can be handled by the RAM of your server or desktop.


example.prt
______________________________________________________
5/6/1

InDep0/
InDep1/
InDep2/
InDep3/miss
InDep4/99

Dep0/
Dep1/
Dep2/
Dep3/
Dep4/
Dep5/

Sing0/

1/-5175954396074827776/C9/C1/C8/C11/C1/
1/-5175954396074827776/C6/C2/C9/C12/C2/C1/C1/
1/-5175954396074827776/C2/C19/C17/C12/C2/C17/C0/
1/-5175954396074827776/C1/C5/C6/C11/C10/C8/C0/

2/3073208371991312384/C3/C11/C0/C3/C13/
2/3073208371991312384/C10/C12/C1/C4/C14/C1/C0/
2/3073208371991312384/C7/C13/C2/C6/C9/C12/C0/
_____________________________________________________

output files

There are three important outputfile.

The first one is the .rpt file. It gives a description of all the iterations and the intermediate results in the linkage process.
The second is the .frq file. It gives the frequencies of all the input fields. Check it to be sure your input is as expected.
The third is the .lin file. This file outputs within each pre matched block the linkage probabilities of each dependent record with the dependent one. 

The first field of the .lin file is the block number followed by the serial number of the dependent record. Then the Id of the independent record is given followed by the Id of the candidate dependent record. After that is the probability that the dependent records is a candidate link for the independent one. In the example i record 1 of block 1 the best candidate of the three dependent records in the block. Record 3 has a very low probability of being a candidate.

example.lin
______________________________________________________
1/1/-5175954396074827776/-5175954396074827776/30.41
1/2/-5175954396074827776/-5175954396074827776/15.32
1/3/-5175954396074827776/-5175954396074827776/-0.06
2/1/3073208371991312384/3073208371991312384/30.35
2/2/3073208371991312384/3073208371991312384/-0.08
3/1/-7271074886109370368/-7271074886109370368/30.27
3/2/-7271074886109370368/-7271074886109370368/7.39
3/3/-7271074886109370368/-7271074886109370368/-0.09
3/4/-7271074886109370368/-7271074886109370368/-0.07
3/5/-7271074886109370368/-7271074886109370368/7.88
______________________________________________________

Literature

There is a lot of academic literature on this topic. However the concept record linkage can apply to other linkage methods like linking on Soundex’s and distances.
Next references are about the method used in Axon:
 

