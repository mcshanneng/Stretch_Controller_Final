#include <Adafruit_CircuitPlayground.h>

int analogPinA = 10;
int analogPinB = 11;
int analogValueA;
int analogValueB;

void setup() {
  Serial.begin(9600);
  CircuitPlayground.begin();
}

void loop() {
analogValueA = analogRead(analogPinA);
analogValueB = analogRead(analogPinB);
Serial.println(analogValueB);
}
