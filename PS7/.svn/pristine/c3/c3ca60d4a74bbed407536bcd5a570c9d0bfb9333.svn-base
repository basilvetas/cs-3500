---------------------- Implementation Notes --------------------

Name: Basil Vetas & Lance Petersen
Date: 11/13/14


Questions:

- Our Test1 passes most of the time, but fails sometimes (See Test1_Mod), why could this be?
- Do we need to handle \r ?
- CloseTest2 - should we throw an exception or should it just return a non-null exception in the 
callback of BeginSend()
- Why is SimpleTest not letting me debug - what's up with mre??


- Do we need a try/catch block around socket.EndSend() and socket.EndReceive() since we have one 
around the start methods? Also do we need to start a new callback thread if an exception is caught?
If so, it might be easier to create a helper method for send and receive to do this stuff in catch
since there are lots of exceptions we may catch.



-----------

Name: Basil Vetas & Lance Petersen
Date: 11/8/14

If we already have a try/catch block in the SentBytes method do we need it in the SendComplete method since
it is kind of recursive??


---------------------- Quiz Notes --------------------

3. Describe the private member variables/properties that you will be using to support the operation of BeginSend 
in your StringSocket class.

We will need a Socket variable, an Encoding variable, a Byte[] array to hold the bytes from a given message that
we will send through the socket, and an int to keep track of the bytes we have sent so far. We will also
need either a queue or something that functions like a queue to manage BeginSend() requests as they come in. 

4. Describe the private member variables/properties that you will be using to support the operation of 
BeginReceive in your StringSocket class.

We will need a Socket variable, an Encoding variable, a Byte[] array to hold the bytes from a given message that
we will receive through the socket, and an int to keep track of the bytes we have received so far. We will also
need either a queue or something that functions like a queue to manage BeginReceive() requests as they come in. 

5. Let s be a StringSocket.  Suppose that it the following three calls happen in quick succession from 
different threads:

s.BeginSend(string1, callback1, payload1);
s.BeginSend(string2, callback2, payload2);
s.BeginSend(string3, callback3, payload3);

Explain how your implementation will avoid intermingling the bytes of string1, string2, and string3 as 
they are being transmitted, and how it will decide when to invoke callback1, callback2, and callback3.

6. Let s be a StringSocket.  Suppose that it the following three calls happen in quick succession from 
different threads:

s.BeginReceive(callback1, payload1);
s.BeginReceive(callback2, payload2);
s.BeginReceive(callback3, payload3);

Explain how your implementation will decide how and when to invoke callback1, callback2, and callback3.

7. Let s be a StringSocket.  Suppose that it the following two calls happen simultaneously on two 
different threads:

s.BeginSend(string1, callback1, payload1);
s.BeginSend(string2, callback2, payload2);

Explain how your implementation will prevent the two calls from interfering with each other.

Let s be a StringSocket.  Suppose that after the following call happens, a large number of bytes 
containing multiple newline characters arrive on the underlying socket.

s.BeginReceive(callback1, payload1);

Explain how your implementation will cope with the extra bytes.
