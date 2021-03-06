----Run this proc to run the rest of the proc
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE PROCEDURE CreateQuestionAnswer 
--	-- Add the parameters for the stored procedure here
--	@question varchar(100) = null,
--	@answer varchar(100) = null,
--	@qKey varchar(100) = null,
--	@intakeFormId int = 0
--AS
--BEGIN
--	-- SET NOCOUNT ON added to prevent extra result sets from
--	-- interfering with SELECT statements.
--	SET NOCOUNT ON;

--    -- Insert statements for procedure here
--	INSERT INTO Question Values (@intakeFormId, @question, @qKey, GETDATE(), GETDATE())

--	DECLARE @answerId INT = @@IDENTITY
--	INSERT INTO Answer VALUES (@answer, @answerId, GETDATE(), GETDATE())
--END
--GO


----Clear out any data created by this script
DELETE FROM Answer WHERE AnswerId > 18
DELETE FROM Question WHERE QuestionId > 18
DELETE FROM IntakeForm WHERE PatientId > 10
DELETE FROM Patient WHERE PatientId > 10
DELETE FROM Agent WHERE VendorId > 7
DELETE FROM Vendor WHERE VendorId > 7

  DECLARE @now DATETIME = GETDATE()
  Insert INTO Vendor VALUES ('Test Company 2', 'Biz As', '2606027799', 'Peyton', 'Manning', @now, @now)
  DECLARE @vendorId int = (SELECT TOP 1 VendorId from Vendor ORDER BY CreatedOn DESC)
  
  INSERT INTO Address VALUES ('1212 Main St', 'Apt B', 'Denver', 'CO', '80224', GETDATE(), GETDATE())
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
    exec CreateQuestionAnswer 'MemberId', '2PG0RE3QX32' , 'MemberId', @intakeFormId
  
  
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
  exec CreateQuestionAnswer 'Additional Notes', '' , 'PainNotes' , @intakeFormId
  
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
  
  --Migraine (Rx Only)
  SET @intakeType = 'MigraineRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'How often do you get a sinus/migraine headache?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'How long do they last?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'How long have you experienced headache?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What do you do for relief?', '' , null , @intakeFormId
  
  --Scar (Rx Only)
  SET @intakeType = 'ScarRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'What is the cause of your scar?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you treated the scar before?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Where is your scar located?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'How long have you had this scar?', '' , null , @intakeFormId
  
  --Heartburn / Acid Reflux (Rx Only)
  SET @intakeType = 'HeartburnAcidRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'How often do you experience heartburn/acid re-flux?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What do you do for relief?', '' , null , @intakeFormId
  
  --Rash / Skin Reflux (Rx Only)
  SET @intakeType = 'RashSkinRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Where is your rash / skin irritation located?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'How long have you had it?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What is the cause of your skin irritation?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you treated the skin irritation before?', '' , null , @intakeFormId
  
  --Anti-Fungal (Rx Only)
  SET @intakeType = 'AntiFungalRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Where is the skin issue located?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'How long have you had this skin issue?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you previously treated the skin issue?', '' , null , @intakeFormId
  
  --Dry Mouth (Rx Only)
  SET @intakeType = 'DryMouthRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Do you know what the cause of your dry mouth is?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you had any treatments or do you take any medications specifically for dry mouth?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'How long have you experienced dry mouth?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'When do you experience dry mouth the most?', '' , null , @intakeFormId
  
  --General (Rx Only)
  SET @intakeType = 'GeneralRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Location(s) of pain:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you get migraine or sinus headaches?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have any rashes or scars on your body?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have chronic heartburn or acid reflux?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you experience dry mouth?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What is your height:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What is your weight:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What current medications are you taking?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Are you Diabetic?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you take oral or insulin to treat diabetes?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have any allergies:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you seen the doctor in the last 12 months:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Any recent medical issue related to your heart, liver, or kidneys?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you had a liver test or liver function test?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have a history of coronary heart disease', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you in the past or currently have a fungal infection such as athlete''s foot or general fungus between your toes?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you suffer from any of the following fungal infections of the skin:', '' , null , @intakeFormId
  
 --General (DME & Rx)
  SET @intakeType = 'GeneralDmeAndRx'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Location(s) of pain:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you get migraine or sinus headaches?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have any rashes or scars on your body?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have chronic heartburn or acid reflux?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you experience dry mouth?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What is your height:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What is your weight:', '' , null , @intakeFormId  
  exec CreateQuestionAnswer 'What is your show size?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What is your waist size?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Are you Diabetic?', '' , null , @intakeFormId  
  exec CreateQuestionAnswer 'Do you take oral or insulin to treat diabetes?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'What current medications are you taking?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have any allergies:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you seen the doctor in the last 12 months:', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Any recent medical issue related to your heart, liver, or kidneys?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you had a liver test or liver function test?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have a history of coronary heart disease', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Have you been prescribed a brace in the last 5 years?', '' , null , @intakeFormId
 
 --Footbath (Rx Only)
  SET @intakeType = 'FootbathRxOnly'
  INSERT INTO IntakeForm VALUES(@patientId, @intakeType, @now, @now)
  SET @intakeFormId =  (SELECT TOP 1 IntakeFormId FROM IntakeForm WHERE IntakeFormType = @intakeType ORDER BY CreatedOn DESC)
  exec CreateQuestionAnswer 'Are you experiencing itching, stinging, and or burning between your toes or on the soles of your feet?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have blisters on your feet that itch?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have cracking and peeling skin on your feet, especially between your toes and on your soles?', '' , null , @intakeFormId
  exec CreateQuestionAnswer 'Do you have dry skin on your soles?', '' , null , @intakeFormId
  