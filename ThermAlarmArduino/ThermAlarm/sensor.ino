uint8_t getPirData(void) { //EXAMPLE INCLUDES MORE COMPLEX PIR VALUE DEFINITION
   uint8_t pirStatus = digitalRead(PIR_PIN);
   //uint8_t pirStatustus = HIGH; //TODO - switch with upper row
   if(pirStatus == HIGH) {
      Serial.println("PIR - Motion detected.");//DEBUG
   }
   if(pirStatus == LOW) {
      Serial.println("PIR - Motion ended."); //DEBUG
   }
   return pirStatus;
}

float* getThermData() {
  float thermData[THERMAL_ARRAY_SIZE];
  for (int i = 0; i < THERMAL_ARRAY_SIZE; i++) {
    thermData[i] = grideye.getPixelTemperature(i);
  }
  return thermData;
}

