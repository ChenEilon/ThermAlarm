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

char* getBtData(char *buf, int buf_len) {
  char *prefix = "+INQ:";
  char suffix = ',';
  int i = 0;
  char c;
  int state = 0;
  while (btSerial.available() and i < buf_len) {
    c = btSerial.read();
    switch (state) {
      case 0:
      case 1:
      case 2:
      case 3:
      case 4:
        if (c == prefix[state])
          state++;
        else
          state = 0;
        break;
      case 5:
        buf[i] = c;
        i++;
        if (c == suffix)
          state = 0;
        break;
    }
  }
  if (i)
    buf[i-1] = '\0';
  else
    buf[0] = '\0';
  return buf;
}

