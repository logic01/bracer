

  DECLARE @now DATETIME = GETDATE()
  Insert INTO Vendor VALUES ('Test Company 2', 'Biz As', '2606027799', 'Peyton', 'Manning', @now, @now)
  DECLARE @vendorId int = (SELECT TOP 1 VendorId from Vendor ORDER BY CreatedOn DESC)
  DECLARE @addressId int = (SELECT TOP 1 AddressId from Address ORDER BY CreatedOn DESC)
  INSERT INTO Agent Values(24, @vendorId, 'Shane Agent', 'Manning Agent', @now, @now)
  INSERT INTO Patient Values(24, 'English', 'Male', 'DME', 'PRIVATE', 'UNIVERSALRX', 'Joe', '', 'Flacco', '1985-04-03 06:00:00.0000000', '2606027799', 1,
							'Afternoon', 1, 'ad', 'Random Notes', '3', 'Mantis', '123456789', @addressId, @addressId, @now, @now)
  DECLARE @patientId int = (SELECT TOP 1 PatientId from Patient ORDER BY CreatedOn DESC)
  
  --Patient Info
  DECLARE @intakeType VARCHAR(20) = 'PatientInfo'
  DECLARE @intakeFormId int = 0
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId = (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  --exec CreateQuestionAnswer 'Height', '5''7' , 'Height', @intakeFormId
  --exec CreateQuestionAnswer 'Weight', '160' , 'Weight', @intakeFormId
  exec CreateQuestionAnswer 'MemberId', '2PG0RE3QX32' , 'MemberId', @intakeFormId
  --exec CreateQuestionAnswer 'Allergies', 'Peanuts, Dairy' , 'Allergies', @intakeFormId
  
  --General DME Only
  SET @intakeType = 'GeneralDmeOnly' 
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId = (SELECT TOP 1 IntakeFormId From IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Have you seen your primary care physician within last year?', 'YES' , null, @intakeFormId
  exec CreateQuestionAnswer 'Have you been prescribed any type of brace within the last 5 years?', 'YES' , null, @intakeFormId
  exec CreateQuestionAnswer 'What is your Height?', '5''7' , 'Height', @intakeFormId
  exec CreateQuestionAnswer 'What is your Weight?', '160' , 'Weight', @intakeFormId
  exec CreateQuestionAnswer 'What is your shoe size?', '11' , 'ShoeSize', @intakeFormId
  exec CreateQuestionAnswer 'What is your waist size?', '32 inches' , 'Waist', @intakeFormId
  exec CreateQuestionAnswer 'Current Medications', 'Heart Meds, Blood Pressure, Ops' , null, @intakeFormId
  exec CreateQuestionAnswer 'Do you have any allergies?', 'Dairy, LYRICA' , 'Allergies', @intakeFormId
  exec CreateQuestionAnswer 'Are you Diabetic?', 'No' , null, @intakeFormId
  exec CreateQuestionAnswer 'Do you take insulin or oral medications for diabetes?', '' , null, @intakeFormId
  
  --Pain DME Only
  SET @intakeType = 'PainDmeOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId = (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Pain Chart', 'Lower Back' , 'PainChart' , @intakeFormId
  exec CreateQuestionAnswer 'Additional Notes', '' , null , @intakeFormId
  
  --Pain Rx Only
  SET @intakeType = 'PainRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Cause of Patient Pain', 'Deadlifts' , 'PainFeeling' , @intakeFormId
  exec CreateQuestionAnswer 'Location(s) of Pain', 'Lower Back, Muscle' , 'PainChart' , @intakeFormId
  exec CreateQuestionAnswer 'Onset of pain (When did the pain begin?)', '2017' , 'PainBegan' , @intakeFormId
  exec CreateQuestionAnswer 'What Provokes Pain', 'Bending forward at 90 degree angle' , 'PainCause' , @intakeFormId
  exec CreateQuestionAnswer 'What currently relieves the pain', 'Massage, Meds' , 'PainSelfTreatment' , @intakeFormId
  exec CreateQuestionAnswer 'Description of Pain', 'Sharp' , 'PainDescription' , @intakeFormId
  exec CreateQuestionAnswer 'Duration of Pain', 'Lower Back, Muscle' , 'PainDuration' , @intakeFormId
  exec CreateQuestionAnswer 'Previous Helpful Treatments', 'Rest, Stretch' , 'PreviousTreatment' , @intakeFormId
  exec CreateQuestionAnswer 'Affects Activities of Daily Living', 'Yes' , 'EffectsDaily' , @intakeFormId
  exec CreateQuestionAnswer 'Surgery in Area', '2' , 'Surgies' , @intakeFormId
  exec CreateQuestionAnswer 'Type of Surgery', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Date of Surgery', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Pain Rating', '5' , 'PainLevel' , @intakeFormId

  --TODO complete Pain Rx, Migraine Rx only, Scar Rx only, Rash/Skin Irritation, Anti-Fungal, Dry Mouth, general rx, general dme n rx, footbath
  

 