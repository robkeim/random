all: functions.o
	g++ p4.cpp functions.o -o p4exe

functions: functions.cpp
	g++ -c functions.cpp
	
submit:
	rm -rf *.o p4exe submission.tar.gz
	tar -zcf submission.tar.gz *

clean:
	rm -rf *.o p4exe submission.tar.gz
