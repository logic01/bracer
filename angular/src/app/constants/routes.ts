export const RouteUrls = {
  LoginComponent: 'login',

  VendorIntakeDocumentComponent: 'vendor/:vendorId/intake-document/:intakeFormId',
  PhysicianIntakeDocumentComponent: 'physician/:physicianId/intake-document/:intakeFormId',


  AgentDashboardComponent: 'agent',

  AgentEditComponent: 'agent/edit/:id',
  AgentCreateComponent: 'account/create/agent',

  PatientCreateComponent: 'agent/patient',
  IntakeFormComponent: 'agent/patient/:id/intake-form',

  PhysicianDashboardComponent: 'physician',
  PhysicianCreateComponent: 'account/create/physician',
  PhysicianEditComponent: 'physician/edit/:id',

  AdminDashboardComponent: 'admin',
  AdminCreateComponent: 'account/create/admin',
  AdminEditComponent: 'admin/edit/:id',

  VendorCreateComponent: 'admin/create/vendor',
  VendorEditComponent: 'vendor/:id/edit',
  VendorViewComponent: 'vendor/:id/view'
};
