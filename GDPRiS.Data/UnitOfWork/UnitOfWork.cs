using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDPRiS.Data.Model;
using GDPRiS.Data.Repository;

namespace GDPRiS.Data
{
    public class UnitOfWork : IDisposable
    {
        #region Fields

        /// <summary>
        /// Data context
        /// </summary>
        private GDPRiSDBContext context;

        private GenericRepository<Users> userRepository;
        private GenericRepository<Phones> phoneRepository;
        private GenericRepository<Companies> companyRepository;
        private GenericRepository<Employees> employeeRepository;
        private GenericRepository<Cars> carRepository;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Data context
        /// </summary>
        public GDPRiSDBContext DataContext
        {
            get
            {
                return context ?? (context = new GDPRiSDBContext());
            }
        }

        #region Repository

        public GenericRepository<Users> UserRepository
        {
            get
            {
                return userRepository ?? (userRepository = new GenericRepository<Users>(DataContext));
            }
        }

        public GenericRepository<Phones> PhoneRepository
        {
            get
            {
                return phoneRepository ?? (phoneRepository = new GenericRepository<Phones>(DataContext));
            }
        }

        public GenericRepository<Employees> EmployeeRepository
        {
            get
            {
                return employeeRepository ?? (employeeRepository = new GenericRepository<Employees>(DataContext));
            }
        }
        public GenericRepository<Companies> CompanyRepository
        {
            get
            {
                return companyRepository ?? (companyRepository = new GenericRepository<Companies>(DataContext));
            }
        }

        public GenericRepository<Cars> CarRepository
        {
            get
            {
                return carRepository ?? (carRepository = new GenericRepository<Cars>(DataContext));
            }
        }
        #endregion Repository

        #endregion Properties

        #region Methods

        /// <summary>
        /// Save changes for unit of work async
        /// </summary>
        public async Task SaveAsync()
        {
            context.ChangeTracker.DetectChanges();
            await context.SaveChangesAsync();
        }

        public void Save()
        {
            try
            {
                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Methods


        #region IDisposable Members

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context?.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose objects
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}
