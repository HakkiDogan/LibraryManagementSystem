using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class PublisherManager : IPublisherService
	{
		IPublisherDal _publisherDal;

		public PublisherManager(IPublisherDal publisherDal)
		{
			_publisherDal = publisherDal;
		}

		public Publisher GetById(int id)
		{
			return _publisherDal.Get(p => p.PublisherId == id);
		}

		public List<Publisher> GetAll()
		{
			return _publisherDal.GetAll().Where(w => w.IsActive).ToList();
		}
	}
}
