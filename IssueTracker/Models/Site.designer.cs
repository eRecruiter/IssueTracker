﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IssueTracker.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="IssueTracker")]
	public partial class SiteDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    partial void InsertComment(Comment instance);
    partial void UpdateComment(Comment instance);
    partial void DeleteComment(Comment instance);
    partial void InsertIssue(Issue instance);
    partial void UpdateIssue(Issue instance);
    partial void DeleteIssue(Issue instance);
    partial void InsertDiscardRule(DiscardRule instance);
    partial void UpdateDiscardRule(DiscardRule instance);
    partial void DeleteDiscardRule(DiscardRule instance);
    #endregion
		
		public SiteDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SQLSERVER_CONNECTION_STRING"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SiteDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
		
		public System.Data.Linq.Table<Comment> Comments
		{
			get
			{
				return this.GetTable<Comment>();
			}
		}
		
		public System.Data.Linq.Table<Issue> Issues
		{
			get
			{
				return this.GetTable<Issue>();
			}
		}
		
		public System.Data.Linq.Table<Status> Status
		{
			get
			{
				return this.GetTable<Status>();
			}
		}
		
		public System.Data.Linq.Table<IssueView> IssueViews
		{
			get
			{
				return this.GetTable<IssueView>();
			}
		}
		
		public System.Data.Linq.Table<DiscardRule> DiscardRules
		{
			get
			{
				return this.GetTable<DiscardRule>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="[User]")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _OpenId;
		
		private string _Email;
		
		private string _Name;
		
		private string _Password;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUsernameChanging(string value);
    partial void OnUsernameChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    #endregion
		
		public User()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OpenId", DbType="nvarchar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string Username
		{
			get
			{
				return this._OpenId;
			}
			set
			{
				if ((this._OpenId != value))
				{
					this.OnUsernameChanging(value);
					this.SendPropertyChanging();
					this._OpenId = value;
					this.SendPropertyChanged("Username");
					this.OnUsernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="nvarchar(50) NOT NULL", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute()]
	public partial class Comment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _IssueId;
		
		private string _Creator;
		
		private System.DateTime _DateOfCreation;
		
		private string _Text;
		
		private string _AttachmentFileName;
		
		private string _AttachmentNiceName;
		
		private System.Nullable<int> _DuplicateIssueId;
		
		private string _Email;
		
		private string _OldStatus;
		
		private string _NewStatus;
		
		private string _OldAssignedTo;
		
		private string _NewAssignedTo;
		
		private EntityRef<Issue> _Issue;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnIssueIdChanging(int value);
    partial void OnIssueIdChanged();
    partial void OnCreatorChanging(string value);
    partial void OnCreatorChanged();
    partial void OnDateOfCreationChanging(System.DateTime value);
    partial void OnDateOfCreationChanged();
    partial void OnTextChanging(string value);
    partial void OnTextChanged();
    partial void OnAttachmentFileNameChanging(string value);
    partial void OnAttachmentFileNameChanged();
    partial void OnAttachmentNiceNameChanging(string value);
    partial void OnAttachmentNiceNameChanged();
    partial void OnDuplicateIssueIdChanging(System.Nullable<int> value);
    partial void OnDuplicateIssueIdChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnOldStatusChanging(string value);
    partial void OnOldStatusChanged();
    partial void OnNewStatusChanging(string value);
    partial void OnNewStatusChanged();
    partial void OnOldAssignedToChanging(string value);
    partial void OnOldAssignedToChanged();
    partial void OnNewAssignedToChanging(string value);
    partial void OnNewAssignedToChanged();
    #endregion
		
		public Comment()
		{
			this._Issue = default(EntityRef<Issue>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IssueId", DbType="Int NOT NULL")]
		public int IssueId
		{
			get
			{
				return this._IssueId;
			}
			set
			{
				if ((this._IssueId != value))
				{
					if (this._Issue.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIssueIdChanging(value);
					this.SendPropertyChanging();
					this._IssueId = value;
					this.SendPropertyChanged("IssueId");
					this.OnIssueIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Creator", DbType="NVarChar(4000) NOT NULL", CanBeNull=false)]
		public string Creator
		{
			get
			{
				return this._Creator;
			}
			set
			{
				if ((this._Creator != value))
				{
					this.OnCreatorChanging(value);
					this.SendPropertyChanging();
					this._Creator = value;
					this.SendPropertyChanged("Creator");
					this.OnCreatorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateOfCreation", DbType="DateTime NOT NULL")]
		public System.DateTime DateOfCreation
		{
			get
			{
				return this._DateOfCreation;
			}
			set
			{
				if ((this._DateOfCreation != value))
				{
					this.OnDateOfCreationChanging(value);
					this.SendPropertyChanging();
					this._DateOfCreation = value;
					this.SendPropertyChanged("DateOfCreation");
					this.OnDateOfCreationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NText NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._Text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AttachmentFileName", DbType="NVarChar(4000)")]
		public string AttachmentFileName
		{
			get
			{
				return this._AttachmentFileName;
			}
			set
			{
				if ((this._AttachmentFileName != value))
				{
					this.OnAttachmentFileNameChanging(value);
					this.SendPropertyChanging();
					this._AttachmentFileName = value;
					this.SendPropertyChanged("AttachmentFileName");
					this.OnAttachmentFileNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AttachmentNiceName", DbType="NVarChar(4000)")]
		public string AttachmentNiceName
		{
			get
			{
				return this._AttachmentNiceName;
			}
			set
			{
				if ((this._AttachmentNiceName != value))
				{
					this.OnAttachmentNiceNameChanging(value);
					this.SendPropertyChanging();
					this._AttachmentNiceName = value;
					this.SendPropertyChanged("AttachmentNiceName");
					this.OnAttachmentNiceNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DuplicateIssueId", DbType="Int")]
		public System.Nullable<int> DuplicateIssueId
		{
			get
			{
				return this._DuplicateIssueId;
			}
			set
			{
				if ((this._DuplicateIssueId != value))
				{
					this.OnDuplicateIssueIdChanging(value);
					this.SendPropertyChanging();
					this._DuplicateIssueId = value;
					this.SendPropertyChanged("DuplicateIssueId");
					this.OnDuplicateIssueIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(4000)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OldStatus", DbType="NVarChar(4000)")]
		public string OldStatus
		{
			get
			{
				return this._OldStatus;
			}
			set
			{
				if ((this._OldStatus != value))
				{
					this.OnOldStatusChanging(value);
					this.SendPropertyChanging();
					this._OldStatus = value;
					this.SendPropertyChanged("OldStatus");
					this.OnOldStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NewStatus", DbType="NVarChar(4000)")]
		public string NewStatus
		{
			get
			{
				return this._NewStatus;
			}
			set
			{
				if ((this._NewStatus != value))
				{
					this.OnNewStatusChanging(value);
					this.SendPropertyChanging();
					this._NewStatus = value;
					this.SendPropertyChanged("NewStatus");
					this.OnNewStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OldAssignedTo", DbType="nvarchar(50)")]
		public string OldAssignedTo
		{
			get
			{
				return this._OldAssignedTo;
			}
			set
			{
				if ((this._OldAssignedTo != value))
				{
					this.OnOldAssignedToChanging(value);
					this.SendPropertyChanging();
					this._OldAssignedTo = value;
					this.SendPropertyChanged("OldAssignedTo");
					this.OnOldAssignedToChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NewAssignedTo", DbType="nvarchar(50)", CanBeNull=false, IsPrimaryKey=true)]
		public string NewAssignedTo
		{
			get
			{
				return this._NewAssignedTo;
			}
			set
			{
				if ((this._NewAssignedTo != value))
				{
					this.OnNewAssignedToChanging(value);
					this.SendPropertyChanging();
					this._NewAssignedTo = value;
					this.SendPropertyChanged("NewAssignedTo");
					this.OnNewAssignedToChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_Comment", Storage="_Issue", ThisKey="IssueId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Issue Issue
		{
			get
			{
				return this._Issue.Entity;
			}
			set
			{
				Issue previousValue = this._Issue.Entity;
				if (((previousValue != value) 
							|| (this._Issue.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Issue.Entity = null;
						previousValue.Comments.Remove(this);
					}
					this._Issue.Entity = value;
					if ((value != null))
					{
						value.Comments.Add(this);
						this._IssueId = value.Id;
					}
					else
					{
						this._IssueId = default(int);
					}
					this.SendPropertyChanged("Issue");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute()]
	public partial class Issue : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Creator;
		
		private System.DateTime _DateOfCreation;
		
		private string _Status;
		
		private string _Text;
		
		private string _StackTrace;
		
		private string _ServerVariables;
		
		private System.Nullable<int> _ParentIssueId;
		
		private bool _IsPublic;
		
		private string _AssignedTo;
		
		private EntitySet<Comment> _Comments;
		
		private EntitySet<Issue> _Issues;
		
		private EntityRef<Issue> _Issue1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCreatorChanging(string value);
    partial void OnCreatorChanged();
    partial void OnDateOfCreationChanging(System.DateTime value);
    partial void OnDateOfCreationChanged();
    partial void OnStatusChanging(string value);
    partial void OnStatusChanged();
    partial void OnTextChanging(string value);
    partial void OnTextChanged();
    partial void OnStackTraceChanging(string value);
    partial void OnStackTraceChanged();
    partial void OnServerVariablesChanging(string value);
    partial void OnServerVariablesChanged();
    partial void OnParentIssueIdChanging(System.Nullable<int> value);
    partial void OnParentIssueIdChanged();
    partial void OnIsPublicChanging(bool value);
    partial void OnIsPublicChanged();
    partial void OnAssignedToChanging(string value);
    partial void OnAssignedToChanged();
    #endregion
		
		public Issue()
		{
			this._Comments = new EntitySet<Comment>(new Action<Comment>(this.attach_Comments), new Action<Comment>(this.detach_Comments));
			this._Issues = new EntitySet<Issue>(new Action<Issue>(this.attach_Issues), new Action<Issue>(this.detach_Issues));
			this._Issue1 = default(EntityRef<Issue>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Creator", DbType="NVarChar(4000) NOT NULL", CanBeNull=false)]
		public string Creator
		{
			get
			{
				return this._Creator;
			}
			set
			{
				if ((this._Creator != value))
				{
					this.OnCreatorChanging(value);
					this.SendPropertyChanging();
					this._Creator = value;
					this.SendPropertyChanged("Creator");
					this.OnCreatorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateOfCreation", DbType="DateTime NOT NULL")]
		public System.DateTime DateOfCreation
		{
			get
			{
				return this._DateOfCreation;
			}
			set
			{
				if ((this._DateOfCreation != value))
				{
					this.OnDateOfCreationChanging(value);
					this.SendPropertyChanging();
					this._DateOfCreation = value;
					this.SendPropertyChanged("DateOfCreation");
					this.OnDateOfCreationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="NVarChar(4000) NOT NULL", CanBeNull=false)]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NVarChar(4000) NOT NULL", CanBeNull=false)]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._Text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StackTrace", DbType="VarChar(8000)")]
		public string StackTrace
		{
			get
			{
				return this._StackTrace;
			}
			set
			{
				if ((this._StackTrace != value))
				{
					this.OnStackTraceChanging(value);
					this.SendPropertyChanging();
					this._StackTrace = value;
					this.SendPropertyChanged("StackTrace");
					this.OnStackTraceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ServerVariables", DbType="VarChar(8000)")]
		public string ServerVariables
		{
			get
			{
				return this._ServerVariables;
			}
			set
			{
				if ((this._ServerVariables != value))
				{
					this.OnServerVariablesChanging(value);
					this.SendPropertyChanging();
					this._ServerVariables = value;
					this.SendPropertyChanged("ServerVariables");
					this.OnServerVariablesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParentIssueId", DbType="Int")]
		public System.Nullable<int> ParentIssueId
		{
			get
			{
				return this._ParentIssueId;
			}
			set
			{
				if ((this._ParentIssueId != value))
				{
					if (this._Issue1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnParentIssueIdChanging(value);
					this.SendPropertyChanging();
					this._ParentIssueId = value;
					this.SendPropertyChanged("ParentIssueId");
					this.OnParentIssueIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsPublic", DbType="Bit NOT NULL")]
		public bool IsPublic
		{
			get
			{
				return this._IsPublic;
			}
			set
			{
				if ((this._IsPublic != value))
				{
					this.OnIsPublicChanging(value);
					this.SendPropertyChanging();
					this._IsPublic = value;
					this.SendPropertyChanged("IsPublic");
					this.OnIsPublicChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AssignedTo", DbType="nvarchar(50)")]
		public string AssignedTo
		{
			get
			{
				return this._AssignedTo;
			}
			set
			{
				if ((this._AssignedTo != value))
				{
					this.OnAssignedToChanging(value);
					this.SendPropertyChanging();
					this._AssignedTo = value;
					this.SendPropertyChanged("AssignedTo");
					this.OnAssignedToChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_Comment", Storage="_Comments", ThisKey="Id", OtherKey="IssueId")]
		public EntitySet<Comment> Comments
		{
			get
			{
				return this._Comments;
			}
			set
			{
				this._Comments.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_Issue", Storage="_Issues", ThisKey="Id", OtherKey="ParentIssueId")]
		public EntitySet<Issue> ChildIssues
		{
			get
			{
				return this._Issues;
			}
			set
			{
				this._Issues.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Issue_Issue", Storage="_Issue1", ThisKey="ParentIssueId", OtherKey="Id", IsForeignKey=true)]
		public Issue ParentIssue
		{
			get
			{
				return this._Issue1.Entity;
			}
			set
			{
				Issue previousValue = this._Issue1.Entity;
				if (((previousValue != value) 
							|| (this._Issue1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Issue1.Entity = null;
						previousValue.ChildIssues.Remove(this);
					}
					this._Issue1.Entity = value;
					if ((value != null))
					{
						value.ChildIssues.Add(this);
						this._ParentIssueId = value.Id;
					}
					else
					{
						this._ParentIssueId = default(Nullable<int>);
					}
					this.SendPropertyChanged("ParentIssue");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Comments(Comment entity)
		{
			this.SendPropertyChanging();
			entity.Issue = this;
		}
		
		private void detach_Comments(Comment entity)
		{
			this.SendPropertyChanging();
			entity.Issue = null;
		}
		
		private void attach_Issues(Issue entity)
		{
			this.SendPropertyChanging();
			entity.ParentIssue = this;
		}
		
		private void detach_Issues(Issue entity)
		{
			this.SendPropertyChanging();
			entity.ParentIssue = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute()]
	public partial class Status
	{
		
		private string _Name;
		
		private bool _Reactivate;
		
		public Status()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(4000) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Reactivate", DbType="Bit NOT NULL")]
		public bool Reactivate
		{
			get
			{
				return this._Reactivate;
			}
			set
			{
				if ((this._Reactivate != value))
				{
					this._Reactivate = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute()]
	public partial class IssueView
	{
		
		private int _Id;
		
		private string _Creator;
		
		private System.DateTime _DateOfCreation;
		
		private string _Status;
		
		private string _Text;
		
		private System.Nullable<int> _ParentIssueId;
		
		private bool _IsPublic;
		
		private string _StackTrace;
		
		private string _ServerVariables;
		
		private System.Nullable<int> _NumberOfChildIssues;
		
		private System.Nullable<int> _NumberOfAttachments;
		
		private System.Nullable<int> _NumberOfComments;
		
		private System.Nullable<System.DateTime> _DateOfUpdate;
		
		private string _AssignedTo;
		
		public IssueView()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Creator", DbType="NVarChar(4000) NOT NULL", CanBeNull=false)]
		public string Creator
		{
			get
			{
				return this._Creator;
			}
			set
			{
				if ((this._Creator != value))
				{
					this._Creator = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateOfCreation", DbType="DateTime NOT NULL")]
		public System.DateTime DateOfCreation
		{
			get
			{
				return this._DateOfCreation;
			}
			set
			{
				if ((this._DateOfCreation != value))
				{
					this._DateOfCreation = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="NVarChar(4000) NOT NULL", CanBeNull=false)]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this._Status = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NVarChar(4000) NOT NULL", CanBeNull=false)]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this._Text = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParentIssueId", DbType="Int")]
		public System.Nullable<int> ParentIssueId
		{
			get
			{
				return this._ParentIssueId;
			}
			set
			{
				if ((this._ParentIssueId != value))
				{
					this._ParentIssueId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsPublic", DbType="Bit NOT NULL")]
		public bool IsPublic
		{
			get
			{
				return this._IsPublic;
			}
			set
			{
				if ((this._IsPublic != value))
				{
					this._IsPublic = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StackTrace", DbType="VarChar(8000)")]
		public string StackTrace
		{
			get
			{
				return this._StackTrace;
			}
			set
			{
				if ((this._StackTrace != value))
				{
					this._StackTrace = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ServerVariables", DbType="VarChar(8000)")]
		public string ServerVariables
		{
			get
			{
				return this._ServerVariables;
			}
			set
			{
				if ((this._ServerVariables != value))
				{
					this._ServerVariables = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumberOfChildIssues", DbType="Int")]
		public System.Nullable<int> NumberOfChildIssues
		{
			get
			{
				return this._NumberOfChildIssues;
			}
			set
			{
				if ((this._NumberOfChildIssues != value))
				{
					this._NumberOfChildIssues = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumberOfAttachments", DbType="Int")]
		public System.Nullable<int> NumberOfAttachments
		{
			get
			{
				return this._NumberOfAttachments;
			}
			set
			{
				if ((this._NumberOfAttachments != value))
				{
					this._NumberOfAttachments = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumberOfComments", DbType="Int")]
		public System.Nullable<int> NumberOfComments
		{
			get
			{
				return this._NumberOfComments;
			}
			set
			{
				if ((this._NumberOfComments != value))
				{
					this._NumberOfComments = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateOfUpdate", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateOfUpdate
		{
			get
			{
				return this._DateOfUpdate;
			}
			set
			{
				if ((this._DateOfUpdate != value))
				{
					this._DateOfUpdate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AssignedTo", DbType="nvarchar(50)")]
		public string AssignedTo
		{
			get
			{
				return this._AssignedTo;
			}
			set
			{
				if ((this._AssignedTo != value))
				{
					this._AssignedTo = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="DiscardRules")]
	public partial class DiscardRule : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Text;
		
		private string _Creator;
		
		private string _ServerVariables;
		
		private string _StackTrace;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnTextChanging(string value);
    partial void OnTextChanged();
    partial void OnCreatorChanging(string value);
    partial void OnCreatorChanged();
    partial void OnServerVariablesChanging(string value);
    partial void OnServerVariablesChanged();
    partial void OnStackTraceChanging(string value);
    partial void OnStackTraceChanged();
    #endregion
		
		public DiscardRule()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NVarChar(4000)")]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._Text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Creator", DbType="NVarChar(4000)")]
		public string Creator
		{
			get
			{
				return this._Creator;
			}
			set
			{
				if ((this._Creator != value))
				{
					this.OnCreatorChanging(value);
					this.SendPropertyChanging();
					this._Creator = value;
					this.SendPropertyChanged("Creator");
					this.OnCreatorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ServerVariables", DbType="VarChar(8000)")]
		public string ServerVariables
		{
			get
			{
				return this._ServerVariables;
			}
			set
			{
				if ((this._ServerVariables != value))
				{
					this.OnServerVariablesChanging(value);
					this.SendPropertyChanging();
					this._ServerVariables = value;
					this.SendPropertyChanged("ServerVariables");
					this.OnServerVariablesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StackTrace", DbType="VarChar(8000)")]
		public string StackTrace
		{
			get
			{
				return this._StackTrace;
			}
			set
			{
				if ((this._StackTrace != value))
				{
					this.OnStackTraceChanging(value);
					this.SendPropertyChanging();
					this._StackTrace = value;
					this.SendPropertyChanged("StackTrace");
					this.OnStackTraceChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
