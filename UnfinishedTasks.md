# UnfinishedTasks.md

## Sprint 1 - Completion Analysis Report

After thorough examination of the project, here is the status of each task:

---

## ✅ TASK 1 — N-Tier Architecture

### Status: **PARTIALLY COMPLETED** ⚠️

#### Acceptance Criteria Status:

1. **✅ Controllers never access DbContext directly**
   - ✓ HomeController uses IUserService (properly abstracted)
   - ✓ UserController has proper dependency injection
   - ❌ **ProductController** - DIRECTLY accesses `ApplicationDbContext` (Line 14-15)
   - ❌ **CategoryController** - DIRECTLY accesses `ApplicationDbContext` (Line 8)

2. **❌ All CRUD operations still work**
   - Most are commented out in Product and Category controllers
   - User CRUD operations through service are working

3. **✅ Repository Pattern is implemented**
   - ✓ IGenericRepo interface exists
   - ✓ GenericRepo base class implemented
   - ✓ IUserRepo interface exists
   - ✓ UserRepo implementation exists

4. **✅ Unit of Work is implemented**
   - ✓ IUnitOfWork interface exists
   - ✓ UnitOfWork class implemented
   - ✓ Exposes Users repository

5. **✅ DTOs are used correctly**
   - ✓ RegisterDTO created
   - ✓ LoginDTO created
   - ✓ DisplayUserDTO created
   - ✓ EditUserDTO created
   - ✓ RolesDTO created

6. **✅ AutoMapper is configured**
   - ✓ RegisterMapping created
   - ✓ DisplayUserMapping created
   - ✓ EditUserMapping created
   - ✓ AutoMapper registered in Program.cs

7. **✅ Application builds without errors**
   - ✓ Build successful

#### **INCOMPLETE ITEMS:**

- [ ] **ProductController must use a Service/Repository** instead of accessing DbContext directly
- [ ] **CategoryController must use a Service/Repository** instead of accessing DbContext directly
- [ ] Create **ICategoryService** and **CategoryService** implementation
- [ ] Create **IProductService** and **ProductService** implementation
- [ ] Create **ProductRepository** and implement **IProductRepo**
- [ ] Create **CategoryRepository** and implement **ICategoryRepo**
- [ ] Update **UnitOfWork** to expose Products and Categories repositories
- [ ] Refactor Product and Category CRUD operations into services
- [ ] Uncomment and migrate Product CRUD operations to use services
- [ ] Uncomment and migrate Category CRUD operations to use services
- [ ] Create AutoMapper profiles for Product and Category DTOs
- [ ] Create ProductDTO and CategoryDTO classes

---

## ✅ TASK 2 — Authentication & Authorization

### Status: **PARTIALLY COMPLETED** ⚠️

#### Acceptance Criteria Status:

1. **✅ Users can register**
   - ✓ Register view exists (Register.cshtml)
   - ✓ RegisterAsync method implemented in UserServices
   - ✓ RegisterDTO created with mapping

2. **✅ Users can login**
   - ✓ Login view exists (Login.cshtml)
   - ✓ LoginAsync method implemented in UserServices
   - ✓ LoginDTO created with mapping
   - ✓ SignInManager properly configured

3. **⚠️ Users can logout**
   - ⚠️ Method implementation needed in UserServices
   - ⚠️ SignOut view/action needed in HomeController

4. **❌ Roles are seeded automatically**
   - ❌ No role seeding in Program.cs
   - ❌ Admin and Customer roles are not automatically created on startup
   - ❌ Manual creation is required through RoleManager
   - ✓ RoleManager is injected and ready for seeding

5. **✅ Customers receive the Customer role**
   - ✓ RegisterAsync assigns "Customer" role automatically (Line 35 in UserServices)

6. **⚠️ Admin pages are protected**
   - ⚠️ [Authorize(Roles = "Admin")] on ProductController only
   - ❌ CategoryController is NOT protected
   - ❌ Missing [Authorize] attributes on admin actions
   - ⚠️ HomeController admin actions NOT protected (DisplayUsers, EditUser, Role, etc.)

7. **❌ Unauthorized users are redirected correctly**
   - ❌ No custom unauthorized redirect configured
   - ❌ No policy-based authorization implemented

#### **INCOMPLETE ITEMS:**

- [ ] Implement **Logout functionality** in UserServices
- [ ] Create **role seeding logic** in Program.cs (Admin and Customer roles)
- [ ] Add **[Authorize(Roles = "Admin")]** to CategoryController
- [ ] Add **[Authorize(Roles = "Admin")]** to Product CRUD actions
- [ ] Add **[Authorize(Roles = "Admin")]** to Category CRUD actions
- [ ] Add **[Authorize(Roles = "Admin")]** to HomeController admin methods (DisplayUsers, EditUser, Role)
- [ ] Add **Logout** endpoint and view to HomeController
- [ ] Implement **custom authorization policy** (for policy-based authorization)
- [ ] Configure **unauthorized redirect** to redirect to login page
- [ ] Add logout button to **_Layout.cshtml** or **_LoginPartial.cshtml**

---

## ⚠️ TASK 3 — User Management

### Status: **PARTIALLY COMPLETED** ⚠️

#### Acceptance Criteria Status:

1. **✅ All users are listed**
   - ✓ DisplayUsers view exists (DisplayUsers.cshtml)
   - ✓ GetAllUsersAsync implemented in UserServices
   - ✓ DisplayUsers action in HomeController (Lines 84-88)

2. **✅ Roles can be changed**
   - ✓ ChangeRoleAsync method implemented in UserServices
   - ✓ UpdateUserAsync updates roles (Line 98-104)
   - ✓ EditUser view exists (EditUser.cshtml)
   - ✓ EditUser action in HomeController (Lines 90-99)

3. **✅ Accounts can be locked**
   - ✓ LockUserAsync method implemented in UserServices
   - ✓ IsLocked field in EditUserDTO
   - ✓ UpdateUserAsync handles locking (Line 97)

4. **✅ Accounts can be unlocked**
   - ✓ UnlockUserAsync method implemented in UserServices
   - ✓ UpdateUserAsync handles unlocking (Line 98)

5. **❌ Proper authorization is enforced**
   - ❌ DisplayUsers action NOT protected with [Authorize(Roles = "Admin")]
   - ❌ EditUser action NOT protected
   - ❌ DeleteUser action NOT protected (Line 100-105)
   - ⚠️ Role action NOT protected (Line 68-73)

#### **INCOMPLETE ITEMS:**

- [ ] Add **[Authorize(Roles = "Admin")]** to DisplayUsers action
- [ ] Add **[Authorize(Roles = "Admin")]** to EditUser action (both GET and POST)
- [ ] Add **[Authorize(Roles = "Admin")]** to DeleteUser action
- [ ] Add **[Authorize(Roles = "Admin")]** to Role action (GET and POST)
- [ ] Add **[Authorize(Roles = "Admin")]** to Roles action
- [ ] Verify that DeleteUser prevents self-deletion (prevent admin from deleting their own account)
- [ ] Add authorization check to prevent deleting the last admin

---

## 🎯 PRIORITY ORDER - What to Complete First:

### **HIGH PRIORITY - Blocking Tasks:**

1. **Refactor Product and Category Controllers** to use Services instead of DbContext
   - This is blocking the completion of Task 1
   - Creates services, repositories, and DTOs for Product and Category

2. **Implement Role Seeding** in Program.cs
   - Automatically create Admin and Customer roles on startup
   - This is essential for proper authorization

3. **Protect Admin Pages with [Authorize(Roles = "Admin")]**
   - Add authorization to all sensitive actions
   - Ensures unauthorized users cannot access admin functionality

### **MEDIUM PRIORITY - Important Tasks:**

4. Implement Logout functionality and view
5. Create and register custom Authorization Policy
6. Add authorization checks to User Management actions
7. Implement self-deletion prevention

### **LOW PRIORITY - Enhancement Tasks:**

8. Add searching and pagination to User Management
9. Implement Email Confirmation

---

## 📊 Overall Completion Summary:

| Task | Acceptance Criteria | Completion |
|------|-------------------|-----------|
| **Task 1: N-Tier Architecture** | 5/7 | **71%** |
| **Task 2: Authentication & Authorization** | 4/7 | **57%** |
| **Task 3: User Management** | 4/5 | **80%** |
| **OVERALL** | 13/19 | **68%** |

---

## 🔴 Critical Blockers:

1. **Product & Category Controllers access DbContext directly** - Violates N-Tier Architecture
2. **Admin pages not protected with [Authorize]** - Security vulnerability
3. **Roles not seeded on startup** - Manual role creation required for every new instance
4. **Logout not implemented** - Users cannot sign out

---

## 📋 Recommended Next Steps:

1. Create ProductRepository, CategoryRepository, and corresponding services
2. Implement role seeding in Program.cs
3. Add [Authorize] attributes to all admin pages
4. Implement logout functionality
5. Test all authorization and authentication flows
